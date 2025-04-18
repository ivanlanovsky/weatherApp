﻿using Microsoft.IdentityModel.Tokens;
using WeatherApp.DTO;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IAuthService
    {
        Task<User?> LoginWithCookie(UserCredentials credentials, HttpContext context);

        Task<User?> LoginWithJwt(UserCredentials credentials, HttpContext context);

        Task Logout(HttpContext context);

    }
}
