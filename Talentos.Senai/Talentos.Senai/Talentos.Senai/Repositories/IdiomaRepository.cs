using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class IdiomaRepository : IIdioma
    {
        private TalentosContext ctx = new TalentosContext();
        private readonly FunctionsGeneral _functions = new FunctionsGeneral();
        private readonly string table = "idioma";

        public List<Idioma> Listar() => ctx.Idioma.ToList();

        public TypeMessage Cadastrar(Idioma data)
        {

            if(data.Idioma1 != null && data.Nivel != null && Convert.ToBoolean(data.IdAluno))
            {
                try {
                    ctx.Idioma.Add(data);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");
                    return _functions.replyObject(okMessage, true);
                } catch(Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = _functions.defaultMessage(table, "error");
                    return _functions.replyObject(errorMessage, false);
                }
            } else
            {
                string errorMessage = _functions.defaultMessage(table, "error");
                return _functions.replyObject(errorMessage, false);
            }
        }

        public TypeMessage Atualizar(int id, Idioma data)
        {
            throw new NotImplementedException();
        }

        public TypeMessage Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
