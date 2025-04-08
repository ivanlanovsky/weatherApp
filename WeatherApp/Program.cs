using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WeatherApp.Common;
using WeatherApp.DbContexts;
using WeatherApp.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var key = builder.Configuration.GetValue<string>("SecretKey");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        };
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                // Read token from cookie
                context.Token = context.HttpContext.Request.Cookies["jwt"];
                return Task.CompletedTask;
            }
        };
    });
//.AddCookie(StringConstants.WeatherAppAuth, options =>
//{
//    options.Cookie.Name = StringConstants.WeatherAppAuth;
//    options.ExpireTimeSpan = TimeSpan.FromHours(1);
//    options.Cookie.SameSite = SameSiteMode.None;
//    options.LoginPath = "/api/auth/login";
//    options.Events.OnRedirectToLogin = context =>
//    {
//        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//        return Task.CompletedTask;
//    };

//    options.Events.OnRedirectToAccessDenied = context =>
//    {
//        context.Response.StatusCode = StatusCodes.Status403Forbidden;
//        return Task.CompletedTask;
//    };
//});

builder.Services.AddAuthorization(
options =>
{
    options.AddPolicy(StringConstants.AdminPolicyTitle, policy => policy.RequireUserName(StringConstants.AdminUserName));
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
        policy.WithOrigins("http://localhost:4200")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        Description = "Enter 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
