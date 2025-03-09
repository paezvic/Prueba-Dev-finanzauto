using AuthService.Data.Models;
using AuthService.Services.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de la base de datos
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AuthServiceDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Inyección de dependencias
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<TokenService>();

            // Configuración de JWT
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

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

                 // Habilitar autenticación desde cookies
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         // Si no encuentra el token en la cabecera Authorization, intenta obtenerlo de la cookie
                         var token = context.Request.Cookies["auth_token"];
                         if (!string.IsNullOrEmpty(token))
                         {
                             context.Token = token;
                         }
                         return Task.CompletedTask;
                     }
                 };
             });

            builder.Services.AddAuthorization();

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

            // Swagger con autenticación JWT
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthService API", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Introduce el token en formato: Bearer {token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new List<string>() }
                });
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configurar middlewares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();  // Verifica que esté ANTES de UseAuthorization()
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
