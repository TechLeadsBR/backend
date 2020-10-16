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
        private readonly Functions _functions;
        private readonly AlunoRepository _alunoRepository;
        private readonly string table;

        public IdiomaRepository()
        {
            _functions = new Functions();
            _alunoRepository = new AlunoRepository();
            table = "idioma";
        }

        public List<Idioma> Listar()
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.Idioma
                    .Include(i => i.IdAlunoNavigation)
                    .ToList();
            }
        }

        public Idioma BuscarPorId(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.Idioma.FirstOrDefault(i => i.IdIdioma == id);
            }
        }

        public TypeMessage Cadastrar(Idioma data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                if (data.Idioma1 != null && data.Nivel != null && Convert.ToBoolean(data.IdAluno))
                {
                    Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());

                    if (alunoBuscado == null)
                    {
                        string notFoundMessaege = _functions.defaultMessage(table, "notfound");
                        return _functions.replyObject(notFoundMessaege, false);
                    }
                    else
                    {
                        try
                        {
                            ctx.Idioma.Add(data);
                            ctx.SaveChanges();

                            string okMessage = _functions.defaultMessage(table, "ok");
                            return _functions.replyObject(okMessage, true);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                            string errorMessage = _functions.defaultMessage(table, "error");
                            return _functions.replyObject(errorMessage, false);
                        }
                    }

                }
                else
                {
                    string dataMessage = _functions.defaultMessage(table, "data");
                    return _functions.replyObject(dataMessage, false);
                }
            }
        }

        public TypeMessage Atualizar(int id, Idioma data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                Idioma idiomaBuscado = BuscarPorId(id);

                if (idiomaBuscado != null)
                {
                    Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());

                    if (alunoBuscado != null)
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

                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error);
                            string errorMessage = _functions.defaultMessage(table, "error");
                            return _functions.replyObject(errorMessage, false);
                        }
                    }
                    else
                    {
                        string notFoundMessage = _functions.defaultMessage("aluno", "notfound");
                        return _functions.replyObject(notFoundMessage, false);
                    }
                }
                else
                {
                    string notFoundMessage = _functions.defaultMessage(table, "notfound");
                    return _functions.replyObject(notFoundMessage, false);
                }
            }
        }

        public TypeMessage Deletar(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                Idioma idiomaBuscado = BuscarPorId(id);

                if (idiomaBuscado != null)
                {
                    try
                    {
                        ctx.Remove(idiomaBuscado);
                        ctx.SaveChanges();

                        string okMessage = _functions.defaultMessage(table, "ok");
                        return _functions.replyObject(okMessage, true);

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error);
                        string errorMessage = _functions.defaultMessage(table, "error");
                        return _functions.replyObject(errorMessage, false);
                    }
                }
                else
                {
                    string notFoundMessage = _functions.defaultMessage(table, "notfound");
                    return _functions.replyObject(notFoundMessage, false);
                }
            }
        }
    }
}
