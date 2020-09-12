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

        public string defaultMessage(string table, int id, string typeError)
        {
            if(typeError == "existente" || typeError == "ok" || typeError == "error" || typeError == "notfound")
            {
                return typeError == "existente" ? $"{table} já existente, verifique os dados digitados" : 
                    typeError == "error" ? $"ocorreu um erro no procedimento na tabela {table}" : 
                    typeError == "ok" ? $"{table} id:{id} procedimento com sucesso!" :
                    $"{table} não encontrado(a)";
            }
            else
            {
                return $"parametro esperado: EXISTENTE ou OK ou ERROR";
            }

        }
    }
}
