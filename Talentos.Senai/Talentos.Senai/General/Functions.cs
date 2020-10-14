using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Talentos.Senai.Utilities
{
    public class Functions
    {
        public TypeMessage replyObject(string messageReturn, bool okReturn)
        {
            return new TypeMessage
            {
                message = messageReturn,
                ok = okReturn
            };
        }

        public string defaultMessage(string table, string type)
        {
            type = type.ToLower();

            if(type == "exists" || type == "ok" || type == "error" || type == "notfound" || type == "data")
            {
                if (type == "exists") return $"{table} já existente, verifique os dados digitados";
                if (type == "error") return $"ocorreu um erro no procedimento da tabela {table}";
                if (type == "ok") return $"{table} - sucesso no procedimento";
                if (type == "notfound") return $"{table} não encontrado(a)";
                else return $"{table} - dados invalidos";
            }
            else
            {
                return $"parametro esperado: EXISTENTE ou OK ou ERROR ou NOTFOUND";
            }
        }

        public int GetJtiInBearerToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsontoken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "jti").Value;
            return Convert.ToInt32(jti);
        }
    }
}
