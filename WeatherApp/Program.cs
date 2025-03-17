using Microsoft.EntityFrameworkCore;
using WeatherApp.Common;
using WeatherApp.DbContexts;
using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddAuthentication(StringConstants.WeatherAppAuth).AddCookie(StringConstants.WeatherAppAuth, options =>
{
    options.Cookie.Name = StringConstants.WeatherAppAuth;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAngularApp",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200/")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
    context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
    context.Response.Headers.Append("Access-Control-Allow-Methods", "GET");
    context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type");
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        return;
    }

    await next();
});

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
