using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class AlunoRepository : IAluno
    {
        TalentosContext ctx = new TalentosContext();

        private General _functions;

        public AlunoRepository()
        {
            _functions = new General();
        }

        public List<Aluno> Listar() => ctx.Aluno.ToList();

        public TypeMessage Cadastrar(Aluno data)
        {
            Aluno alunoExistente = ctx.Aluno.FirstOrDefault(a => a.Email == data.Email || a.Rg == data.Rg || a.Cpf == data.Cpf);

            if(alunoExistente == null)
            {
                ctx.Aluno.Add(data);
                ctx.SaveChanges();

                string okMessage = _functions.defaultMessage("aluno", data.IdAluno, "ok");

                return _functions.returnResponse(okMessage, true);
            }else
            {
                string alunoExists = _functions.defaultMessage("aluno", data.IdAluno, "existente");

                return _functions.returnResponse(alunoExists, false);
            }
        }

        public TypeMessage Atualizar(int id, Aluno data)
        {
            throw new NotImplementedException();
        }

        public TypeMessage Deletar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
