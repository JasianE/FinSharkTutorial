using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config) // iconfiguration is an object that pulls from the config file in your project through DI
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])); // big ass line that says new code, using utf8, getbytes, config it to jwt, need to do this for signing.
            // turns it into a symmetricsecuritykey object
        } 
        //Symmetric key is a way to write / encrypt the security sepcific to our server, and cannot be tampered? 
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim> // for claims
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.GivenName, user.UserName), // just creates a new claim for the user and access into the app
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email)

            };
            //key is specified in app.json
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); // this is an object of the token, we haven't actually created it, we have to pass it in first.

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), //tells jwt which perms it has i guess?
                Expires = DateTime.Now.AddDays(7), //sets expiry
                SigningCredentials = creds, //actually the key
                Issuer = _config["JWT:Issuer"], // this fetches the issuer from the app.settings file
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); // writes the token into a strnig
        }
    }
}