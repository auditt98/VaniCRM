using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Backend.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Builder;
using JWT.Exceptions;
using System.Security.Cryptography;
using static Backend.Extensions.Enum;
using System.Resources;
using Backend.Resources;

namespace Backend.Extensions
{
    public class JwtTokenManager
    {
        private const string Secret = "db3OIsj+BXE9kYDy0t5MbecNekrF+2d/1sFnWG4HnA8TZY30iTOdzVWJG8abcFgaGlOgJuQZdcF2Luqm/hccMw==";
        public static string GenerateJwtToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                { "ID", user.ID },
                { "email", user.Email }
            };
            var token = JwtBuilder.Create()
                                  .WithAlgorithm(new HMACSHA256Algorithm())
                                  .WithSecret(Secret)
                                  .AddClaim("id", user.ID)
                                  .AddClaim("email", user.Email)
                                  .ExpirationTime(DateTime.Now.AddHours(1))
                                  .Encode();
            return token;
        }


        /// <summary>
        /// Validate and decode JWT token
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <returns>Dictionary(string, object), access with varname["claimName"], check for error with varname["error"]</returns>
        public static IDictionary<string, object> ValidateJwtToken(string token)
        {
            try
            {
                var json = JwtBuilder.Create()
                                     .WithAlgorithm(new HMACSHA256Algorithm())
                                     .WithSecret(Secret)
                                     .MustVerifySignature()
                                     .Decode<IDictionary<string, object>>(token);
                return json;
            }
            catch (TokenExpiredException)
            {
                var json = new Dictionary<string, object>();
                json.Add("error", ErrorMessages.TOKEN_EXPIRED);
                return json;
            }
            catch (SignatureVerificationException)
            {
                var json = new Dictionary<string, object>();
                json.Add("error", ErrorMessages.TOKEN_INVALID);
                return json;
            }


        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

