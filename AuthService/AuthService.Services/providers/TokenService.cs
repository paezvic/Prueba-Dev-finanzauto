using AuthService.Data.Models;
using AuthService.Services;
using AuthService.Services.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.Providers
{
    /// <summary>
    /// Servicio para la gestión de tokens de autenticación.
    /// Permite obtener, crear y eliminar tokens en la base de datos.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly AuthServiceDbContext _context;

        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(int usuarioId, string correo)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
            var expiration = DateTime.UtcNow.AddMinutes(60);
            //var expiration = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationInMinutes"]!));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

