using Microsoft.EntityFrameworkCore;
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
        private readonly AlunoRepository _alunoRepository = new AlunoRepository();

        public List<Idioma> Listar() => ctx.Idioma.Include(i => i.IdAlunoNavigation).ToList();

        public Idioma BuscarPorId(int id) => ctx.Idioma.FirstOrDefault(i => i.IdIdioma == id);

        public TypeMessage Cadastrar(Idioma data)
        {

            if(data.Idioma1 != null && data.Nivel != null && Convert.ToBoolean(data.IdAluno))
            {
                Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());

                if(alunoBuscado == null)
                {
                    string notFoundMessaege = _functions.defaultMessage("aluno", "notfound");
                    return _functions.replyObject(notFoundMessaege, false);
                }else
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
                }

            } else
            {
                string errorMessage = _functions.defaultMessage(table, "error");
                return _functions.replyObject(errorMessage, false);
            }
        }

        public TypeMessage Atualizar(int id, Idioma data)
        {
            Idioma idiomaBuscado = BuscarPorId(id);

            if(idiomaBuscado != null)
            {
                try
                {
                    idiomaBuscado.Idioma1 = data.Idioma1 ?? idiomaBuscado.Idioma1;
                    idiomaBuscado.Nivel = data.Nivel ?? idiomaBuscado.Nivel;
                    idiomaBuscado.IdAluno = data.IdAluno ?? idiomaBuscado.IdAluno;

                    ctx.Idioma.Update(idiomaBuscado);
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
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public TypeMessage Deletar(int id)
        {
            Idioma idiomaBuscado = BuscarPorId(id);

            if(idiomaBuscado != null)
            {
                try
                {
                    ctx.Remove(idiomaBuscado);
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
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }
    }
}
