using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISI_TP02_Forms
{
    internal class Metodos
    {
        public Funcionario DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return null;
            }

            var jwtToken = handler.ReadJwtToken(token);

            var pessoaJson = jwtToken.Payload.SerializeToJson();
            var pessoaId = jwtToken.Payload["id"];
            var pessoaData = JsonSerializer.Deserialize<Dictionary<string, object>>(pessoaJson);
            int id = Convert.ToInt32(pessoaId);

            string teste = pessoaData.ContainsKey("NIF") ? pessoaData["NIF"].ToString() : null;

            int nif = Convert.ToInt32(teste);
            Funcionario f = new Funcionario
            {
                Id = id,
                Nome = pessoaData.ContainsKey("Nome") ? pessoaData["Nome"].ToString() : null,
                NIF = nif,
                Jwt = token,
                //Role = pessoaData.ContainsKey("role") ? pessoaData["role"].ToString() : null,
            };
            return f;
        }
    }
}
