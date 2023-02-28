using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrderUp_API.Interfaces;
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
    }
    ).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})

    .AddJwtBearer(options => {

        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters() {
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
        };
    });

builder.Services.AddSignalR();


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

builder.Services.AddScoped<IMailRepository, SendGridRepository>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddSingleton<IUserIdProvider, JwtUserIdProvider>();
builder.Services.AddSingleton<OnlineRestaurantDb>();
builder.Services.AddScoped<NetworkService>();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(opt => {
        opt.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
           .WithOrigins("https://order-up-frontend.vercel.app", "https://localhost:5173");
    });
});

builder.Services.AddScoped<ModelValidationActionFilter>();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => {

    endpoints.MapControllers();

    endpoints.MapHub<ServerHub>("/chat");
});


app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
