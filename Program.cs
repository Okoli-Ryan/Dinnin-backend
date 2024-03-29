using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using OrderUp_API.MessageConsumers;
using OrderUp_API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<OrderUpDbContext>(
    opt => opt.UseMySQL(configuration["OrderUpDatabase"]));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(
    config => {
        config.Filters.Add(new AuthorizationActionFilter());
        config.Filters.Add(new PermissionActionFilter());
    })
    .AddJsonOptions(x => {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });



builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})

    .AddCookie(options => {
        options.AccessDeniedPath = "/access-denied";
        options.LoginPath = "/access-denied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<MenuCategoryRepository>();
builder.Services.AddScoped<MenuItemRepository>();
builder.Services.AddScoped<MenuItemImageRepository>();
builder.Services.AddScoped<RestaurantRepository>();
builder.Services.AddScoped<SavedRestaurantRepository>();
builder.Services.AddScoped<SideItemRepository>();
builder.Services.AddScoped<SidesRepository>();
builder.Services.AddScoped<TableRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderItemRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<VerificationCodeRepository>();
builder.Services.AddScoped<IUserEntityRepository<User>>();
builder.Services.AddScoped<IUserEntityRepository<Admin>>();

builder.Services.AddScoped<AnalyticsService>();
builder.Services.AddScoped<MenuCategoryService>();
builder.Services.AddScoped<MenuItemService>();
builder.Services.AddScoped<MenuItemImageService>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<SavedRestaurantService>();
builder.Services.AddScoped<SideItemService>();
builder.Services.AddScoped<SidesService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<IUserEntityService<User>>();
builder.Services.AddScoped<IUserEntityService<Admin>>();
builder.Services.AddScoped<VerificationCodeService>();

builder.Services.AddScoped<IMailRepository, MailServerlessRepository>();
builder.Services.AddScoped<MailService>();

builder.Services.AddSingleton<IUserIdProvider, JwtUserIdProvider>();
builder.Services.AddScoped<NetworkService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddSingleton<MessageProducerService>();
builder.Services.AddSingleton<PusherService>();
builder.Services.AddScoped<PushNotificationService>();


builder.Services.AddScoped<VerificationQueueHandler<EmailMQModel>>();
builder.Services.AddScoped<ForgotPasswordQueueHandler<EmailMQModel>>();
builder.Services.AddScoped<NewStaffRegistrationQueueHandler<StaffRegistrationModel>>();
builder.Services.AddScoped<PushNotificationQueueHandler<PushNotificationBody>>();


builder.Services.AddHostedService<EmailMessageConsumer>();

builder.Services.AddCors();

builder.Services.AddScoped<ModelValidationActionFilter>();

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.SameSite = SameSiteMode.None; // Set SameSite to None for the sign-in cookie
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.MinimumSameSitePolicy = SameSiteMode.None; // Set SameSite to None for all cookies
    options.HttpOnly = HttpOnlyPolicy.Always; // Ensure cookies are accessible only through HTTP requests
    options.Secure = CookieSecurePolicy.Always; // Require secure connections (HTTPS) for cookies
});


//builder.Services.AddHttpsRedirection(options => {
//    options.HttpsPort = 7282;
//});

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development") {

    var port = Environment.GetEnvironmentVariable("PORT");
    builder.WebHost.UseUrls($"http://*:{port}");
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
}

app.UseCors(options => {
    options
       .WithOrigins("https://order-up-frontend.vercel.app", "https://localhost:7282", "https://dinnin-dashboard.vercel.app", "https://localhost:5173")
       .AllowAnyMethod()
       .AllowAnyHeader()
       .AllowCredentials();
});
app.UseRouting();


//app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseEndpoints(endpoints => {

    endpoints.MapControllers();

});

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<OrderUpDbContext>();
    if (context.Database.GetPendingMigrations().Any()) {
        context.Database.Migrate();
    }
    context.Database.EnsureCreated();
}


using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    var lifeTime = services.GetRequiredService<IHostApplicationLifetime>();

    var rabbitMQProducer = services.GetRequiredService<MessageProducerService>();

    lifeTime.ApplicationStopping.Register(() => {

        rabbitMQProducer.Dispose();
    });
}


app.Run();
