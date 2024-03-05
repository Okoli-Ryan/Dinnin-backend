using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrderUp_API.Interfaces;
using OrderUp_API.MessageConsumers;
using OrderUp_API.Middlewares;
using OrderUp_API.Services;

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
    }
    ).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})

    .AddCookie();

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
builder.Services.AddSingleton<OnlineRestaurantDb>();
builder.Services.AddScoped<NetworkService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<MessageProducerService, MessageProducerService>();
builder.Services.AddScoped<PusherService>();
builder.Services.AddScoped<PushNotificationService>();


builder.Services.AddScoped<VerificationQueueHandler<EmailMQModel>>();
builder.Services.AddScoped<PushNotificationQueueHandler<PushNotificationBody>>();


builder.Services.AddHostedService<EmailMessageConsumer>();

builder.Services.AddCors();

builder.Services.AddScoped<ModelValidationActionFilter>();

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors(options => {
    options
       .WithOrigins("https://order-up-frontend.vercel.app", "https://dinnin-dashboard.vercel.app", "https://localhost:5173", "https://localhost:5001", "https://localhost:7282")
       .WithMethods("GET", "PATCH", "POST", "DELETE", "OPTIONS")
       .AllowAnyHeader()
       .AllowCredentials();
});
app.UseRouting();


app.UseHttpsRedirection();

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
