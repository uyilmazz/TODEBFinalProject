
using Core.Utilities.Security.Encryption;
using Dto.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private TokenOptions _tokenOptions;
        private IConfiguration _configuration;
        private DateTime _expirationTime;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();   
        }

        public AccessToken CreateToken(PersonDetailDto personDetailDto)
        {
            _expirationTime = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateSecurityToken(personDetailDto, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _expirationTime
            };
        }

        public JwtSecurityToken CreateSecurityToken(PersonDetailDto personDetailDto, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: _expirationTime,
                notBefore: DateTime.Now,
                claims: SetClaim(personDetailDto),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaim(PersonDetailDto personDetailDto)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, $"{personDetailDto.FirstName} {personDetailDto.LastName}"));
            claims.Add(new Claim(ClaimTypes.Email, personDetailDto.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, personDetailDto.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, personDetailDto.TypeName));
            return claims;
        }
    }
}
