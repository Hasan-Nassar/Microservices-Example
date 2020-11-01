using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
    public class JWTHandler: IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecuriTyhandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issueSignKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;
        

        public JWTHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _issueSignKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials=new SigningCredentials(_issueSignKey,SecurityAlgorithms.HmacSha256);
            _jwtHeader=new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issueSignKey
            };

        }
        
        public JsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expire = nowUtc.AddMinutes(_options.ExpiryMinutes);
            var centryBegin=new DateTime(1970,1,1).ToUniversalTime();
            var exp = (long) (new TimeSpan(expire.Ticks - centryBegin.Ticks).TotalSeconds);
            var now = (long) (new TimeSpan(nowUtc.Ticks - centryBegin.Ticks).TotalSeconds);
            var payload=new JwtPayload
            {
                {"sub",userId},
                {"iss",_options.Issuer},
                {"iat",now},
                {"exp",exp},
                {"unique_name",userId},
                
            };
            
            var jwt = new JwtSecurityToken(_jwtHeader,payload);
            var token = _jwtSecuriTyhandler.WriteToken(jwt);
            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}