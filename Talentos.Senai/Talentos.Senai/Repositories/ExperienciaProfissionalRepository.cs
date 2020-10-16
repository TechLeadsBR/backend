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
    public class ExperienciaProfissionalRepository : IExperienciaProfissional
    {
        private readonly Functions _functions;
        private IAluno _alunoRepository;
        private readonly string table;

        public ExperienciaProfissionalRepository()
        {
            _functions = new Functions();
            _alunoRepository = new AlunoRepository();
            table = "experienciaProfissional";
        }

        public List<ExperienciaProfissional> Listar(int jti)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.ExperienciaProfissional
                    .Include(f => f.IdAlunoNavigation)
                    .Where(e => e.IdAluno == jti)
                    .ToList();
            }
        }

        public ExperienciaProfissional BuscarPorId(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.ExperienciaProfissional
                    .Include(e => e.IdAluno)
                    .FirstOrDefault(e => e.IdExperienciaProfissional == id);
            }
        }

        public TypeMessage Cadastrar(ExperienciaProfissional data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                if (data != null)
                {
                    Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());

                    if (alunoBuscado != null)
                    {
                        try
                        {
                            ctx.ExperienciaProfissional.Add(data);
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
                    string dataMessage = _functions.defaultMessage(table, "data");
                    return _functions.replyObject(dataMessage, false);
                }
            }
        }

        public TypeMessage Atualizar(int id, ExperienciaProfissional dataExperiencia)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                ExperienciaProfissional experienciaParaAtualizar = BuscarPorId(id);

                if (experienciaParaAtualizar != null)
                {
                    Aluno alunoBuscado = _alunoRepository.BuscarPorId(dataExperiencia.IdAluno.GetValueOrDefault());

                    if (alunoBuscado != null)
                    {
                        try
                        {
                            experienciaParaAtualizar.Empresa = dataExperiencia.Empresa ?? experienciaParaAtualizar.Empresa;
                            experienciaParaAtualizar.Cargo = dataExperiencia.Cargo ?? experienciaParaAtualizar.Cargo;
                            experienciaParaAtualizar.Descricao = dataExperiencia.Descricao ?? experienciaParaAtualizar.Descricao;
                            experienciaParaAtualizar.DataInico = dataExperiencia.DataInico != null ? dataExperiencia.DataInico : experienciaParaAtualizar.DataInico;
                            experienciaParaAtualizar.DataFim = dataExperiencia.DataFim != null ? dataExperiencia.DataFim : experienciaParaAtualizar.DataFim;
                            experienciaParaAtualizar.IdAluno = dataExperiencia.IdAluno ?? experienciaParaAtualizar.IdAluno;

                            ctx.ExperienciaProfissional.Update(experienciaParaAtualizar);
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
                ExperienciaProfissional experienciaParaDeletar = BuscarPorId(id);

                if (experienciaParaDeletar != null)
                {
                    try
                    {
                        ctx.ExperienciaProfissional.Remove(experienciaParaDeletar);
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
