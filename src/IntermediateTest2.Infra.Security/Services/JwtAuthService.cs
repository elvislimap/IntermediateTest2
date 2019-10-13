using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using IntermediateTest2.Domain.ValueObjects;
using IntermediateTest2.Infra.Security.Interfaces;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;

namespace IntermediateTest2.Infra.Security.Services
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;

        public JwtAuthService(TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }


        public string GenerateToken(string uniqueName, DateTime dateTimeCreated, DateTime dateTimeExpires)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(uniqueName, "Login"), new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName)
                });

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dateTimeCreated,
                Expires = dateTimeExpires
            }));
        }

        public static void RegisterLogin(IServiceCollection services, IConfiguration configuration,
            AuthorizationPolicy policy)
        {
            var signingConfigurations = new SigningConfigurations();
            var tokenConfigurations = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);

            services.AddSingleton(signingConfigurations);
            services.AddSingleton(tokenConfigurations);

            RegisterAuthentication(services, signingConfigurations, tokenConfigurations);
            RegisterAuthorization(services, policy);
        }

        private static void RegisterAuthentication(IServiceCollection services,
            SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            services.AddAuthentication(authOpt =>
            {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOpt =>
            {
                bearerOpt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingConfigurations.Key,
                    ValidAudience = tokenConfigurations.Audience,
                    ValidIssuer = tokenConfigurations.Issuer,

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private static void RegisterAuthorization(IServiceCollection services, AuthorizationPolicy policy)
        {
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", policy);
            });
        }
    }
}