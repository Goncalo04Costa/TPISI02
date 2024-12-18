using AsmxService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ISI_TP02_APIRestful.JWT
{
    public class obterJWT
    {
        public string GenerateJwtToken(WCFHospitalService.Funcionario func)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("i14Ff4US29DuEJplXHLFqIHNcy/o+MkyJ0BXQ1uiOLzP+belDIFnREKfYn34NCed");

            var claims = new[]
            {
        new Claim("Nome", func.Nome),
        new Claim("NIF", func.NIF.ToString()),
        new Claim("id", func.Id.ToString()),
        new Claim("TipoFuncionarioId", func.TipoFuncionario.Id.ToString())
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "ISI-TP02-26024-25988",
                Audience = "api",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

