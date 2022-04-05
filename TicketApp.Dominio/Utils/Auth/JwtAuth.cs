using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace TicketApp.Dominio.Utils.Auth
{
    public class JwtAuth
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        public JwtAuth(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public JwtResponse CreateToken(string uniqueName)
        {
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(uniqueName, "UniqueName"), new[]{
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName)
            });

            var createDate = DateTime.Now;
            var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return new JwtResponse()
            {
                AccessToken = handler.WriteToken(securityToken),
                ExpiresAt = expirationDate,
                TokenType = "bearer"
            };
        }
    }
}
