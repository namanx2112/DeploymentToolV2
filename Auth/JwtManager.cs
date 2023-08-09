using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DeploymentTool.Model;

namespace DeploymentTool.Auth
{
    public class JwtManager
    {
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static AuthResponse GenerateToken(User objUser, int expireHours = 24)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            AuthResponse resp = new AuthResponse();
            //var now = DateTime.UtcNow;
            var now = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, objUser.tName),
            new Claim(ClaimTypes.Role, objUser.nRole.ToString()),
            new Claim(ClaimTypes.NameIdentifier, objUser.nUserID.ToString())

        }),

                Expires = now.AddHours(Convert.ToInt32(expireHours)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            resp.Expires = tokenDescriptor.Expires;
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            resp.Token = tokenHandler.WriteToken(stoken);
            resp.IsAuthSuccessful = true;
            return resp;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }

    public class UserForAuthentication
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string tContent { get; set; }
    }

    public class ChangePasswordModel
    {
        public string tCurrentPassword { get; set; }
        public string tNewPassword { get; set; }
    }

    public class AuthResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
    }

    public class UserAccessModel
    {
        public string tPermissionName { get; set; }
        public int nPermVal { get; set; }
    }

    public class UserAccessResponse
    {
        public string tData { get; set; }
    }
}
