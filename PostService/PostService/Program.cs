using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostService.data.Models;
using PostService.service.provider;
using PostService.service.services;
using System.Text;

namespace PostService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<PublicacionesDbContext>(option =>
                option.UseSqlServer(connectionString));

            builder.Services.AddScoped<IPublicacionService, PublicacionService>();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            // Configurar autenticación con JWT desde cookies
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ClockSkew = TimeSpan.Zero
                    };

                    // Extraer el token de la cookie
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("auth_token"))
                            {
                                context.Token = context.Request.Cookies["auth_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();

            // Configuración de CORS
            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Agrega el frontend
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Usar CORS
            app.UseCors("AllowFrontend");

            // Permitir el uso de cookies en autenticación
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
