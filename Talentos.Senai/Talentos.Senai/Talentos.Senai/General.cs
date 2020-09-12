using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talentos.Senai.Utilities
{
    public class General
    {
        public TypeMessage returnResponse(string messageReturn, bool okReturn)
        {
            return new TypeMessage
            {
                message = messageReturn,
                ok = okReturn
            };
        }

        public string defaultMessage(string tabel, int id, string typeError)
        {
            if(typeError == "existente" || typeError == "ok" || typeError == "error")
            {
                return typeError == "existente" ? $"{tabel} já existente, verifique os dados digitados" : 
                    typeError == "error" ? $"ocorreu um erro ao cadastrar {tabel}" : $"{tabel} id:{id} prodecimento com sucesso!";
            }
            else
            {
                return $"parametro esperado: EXISTENTE ou OK ou ERROR";
            }

        }
    }
}
