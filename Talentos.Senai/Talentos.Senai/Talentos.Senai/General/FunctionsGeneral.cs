using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talentos.Senai.Utilities
{
    public class FunctionsGeneral
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
                return type == "exists" ? $"{table} já existente, verifique os dados digitados" :
                    type == "error" ? $"ocorreu um erro no procedimento da tabela {table}" :
                    type == "ok" ? $"{table} - sucesso no procedimento" :
                    type == "notfound" ? $"{table} não encontrado(a)" : $"dados invalidos na tabela {table}";
            }
            else
            {
                return $"parametro esperado: EXISTENTE ou OK ou ERROR ou NOTFOUND";
            }

        }
    }
}
