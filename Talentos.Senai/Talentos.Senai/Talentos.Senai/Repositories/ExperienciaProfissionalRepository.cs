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
            private TalentosContext ctx = new TalentosContext();
            private readonly Functions _functions = new Functions();
            private readonly string table = "experienciaProfissional";

            public List<ExperienciaProfissional> Listar() => ctx.ExperienciaProfissional.Include(f => f.IdAlunoNavigation).ToList();

            public ExperienciaProfissional BuscarPorId(int id) => ctx.ExperienciaProfissional.FirstOrDefault(e => e.IdExperienciaProfissional == id);

            public TypeMessage Cadastrar(ExperienciaProfissional data)
            {
              ExperienciaProfissional experienciaExistente = ctx.ExperienciaProfissional.FirstOrDefault(e => e.IdExperienciaProfissional == data.IdExperienciaProfissional);

                if (experienciaExistente == null)
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
                    string existsMessage = _functions.defaultMessage(table, "exists");
                    return _functions.replyObject(existsMessage, false);
                }
            }

            public TypeMessage Atualizar(int id, ExperienciaProfissional dataExperiencia)
            {
                ExperienciaProfissional experienciaParaAtualizar = BuscarPorId(id);

                if (experienciaParaAtualizar != null)
                {
                    try
                    {
                        experienciaParaAtualizar.Empresa = dataExperiencia.Empresa ?? experienciaParaAtualizar. Empresa;
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
                    string notFoundMessage = _functions.defaultMessage(table, "notfound");
                    return _functions.replyObject(notFoundMessage, false);
                }
            }

            public TypeMessage Deletar(int id)
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
