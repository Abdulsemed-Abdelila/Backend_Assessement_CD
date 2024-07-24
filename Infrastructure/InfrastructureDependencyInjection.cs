using Galacticos.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Persistence.Contracts.Auth;
using Application.Persistence.Contracts.Common;
using Application.Services.Cloudinary;
using Application.Services.ImageUpload;
using Infrastructure.Authentication;
using Infrastructure.Mail;
using Infrastructure.services;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;  
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services,ConfigurationManager configuration)
    {
 
        var jwtSettings = new Jwtsettings();
        // var openAi = new OpenAi();


        configuration.GetSection(Jwtsettings.SectionName).Bind(jwtSettings);
        configuration.GetSection(OpenAi.SectionName).Bind(openAi);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton(Options.Create(openAi));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        // services.AddSingleton<IStringValidator, StringValidator>();
        services.AddScoped<IPasswordService, PasswordService>();
        // services.AddTransient<IEmailSender, EmailSender>();
        
        //services.AddSingleton<IFirebaseService, FirebaseService>();

        services.Configure<CloudinarySetting>(configuration.GetSection(CloudinarySetting.SectionName));
        services.AddTransient<ICloudinaryService, CloudinaryService>();

        //firebase
        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer("FirebaseAuth", opt =>
        //    {
        //        opt.Authority = Environment.GetEnvironmentVariable("Firebase_Issuer");
        //        opt.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = Environment.GetEnvironmentVariable("Firebase_Issuer"),
        //            ValidAudience = Environment.GetEnvironmentVariable("Firebase_Audience")
        //        };
        //    });

        // jwt
        services.AddAuthentication(defaultScheme:JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
            ValidAudience = Environment.GetEnvironmentVariable("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
        });

        //services.AddAuthorization(opt =>
        //{
        //    opt.DefaultPolicy = new AuthorizationPolicyBuilder()
        //    .AddAuthenticationSchemes("FirebaseAuth", "JwtAuth")
        //    .RequireAuthenticatedUser()
        //    .Build();
        //});

        return services;


    }
}
