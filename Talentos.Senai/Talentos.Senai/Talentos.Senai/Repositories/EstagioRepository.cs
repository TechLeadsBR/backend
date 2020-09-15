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
    public class EstagioRepository : IEstagio
    {
        private TalentosContext ctx = new TalentosContext();
        private readonly Functions _functions = new Functions();
        private IEmpresa _empresaRepository = new EmpresaRepository();
        private IAluno _alunoRepository = new AlunoRepository();
        private readonly string table = "estagio";

        public List<Estagio> Listar() => ctx.Estagio.Include(e => e.IdAlunoNavigation).Include(e => e.IdEmpresaNavigation).ToList();

        public Estagio BuscarPorIdAluno(int id) => ctx.Estagio.FirstOrDefault(e => e.IdAluno == id);

        public Estagio BuscarPorId(int id) => ctx.Estagio.FirstOrDefault(e => e.IdEstagio == id);

        public TypeMessage Cadastrar(Estagio data)
        {
            Estagio estagioProcurado = BuscarPorIdAluno(data.IdAluno.GetValueOrDefault());

            if(estagioProcurado == null)
            {
                Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());
                Empresa empresaBuscada = _empresaRepository.BuscarPorId(data.IdEmpresa.GetValueOrDefault());

                if(alunoBuscado != null && empresaBuscada != null)
                {
                    try
                    {
                        ctx.Estagio.Add(data);
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
                } else
                {
                    String dataMessage = _functions.defaultMessage(table, "data");
                    return _functions.replyObject(dataMessage, false);
                }
            } else
            {
                string existsMessage = _functions.defaultMessage(table, "exists");
                return _functions.replyObject(existsMessage, false);
            }
        }
    
        public TypeMessage Atualizar(int id, Estagio data)
        {
            Estagio estagioBuscado = BuscarPorId(id);

            if(estagioBuscado != null)
            {
                Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());
                Empresa empresaBuscada = _empresaRepository.BuscarPorId(data.IdEmpresa.GetValueOrDefault());

                if(alunoBuscado != null && empresaBuscada != null)
                {
                    try
                    {
                        estagioBuscado.Responsavel = data.Responsavel ?? estagioBuscado.Responsavel;
                        estagioBuscado.Inicio = data.Inicio != null ? data.Inicio : estagioBuscado.Inicio;
                        estagioBuscado.Termino = data.Termino != null ? data.Termino : estagioBuscado.Termino;
                        estagioBuscado.StatusContrato = data.StatusContrato ?? estagioBuscado.StatusContrato;
                        estagioBuscado.Documentos = data.Documentos ?? estagioBuscado.Documentos;
                        estagioBuscado.IdEmpresa = data.IdEmpresa ?? estagioBuscado.IdEmpresa;
                        estagioBuscado.IdAluno = data.IdAluno ?? estagioBuscado.IdAluno;

                        ctx.Estagio.Update(estagioBuscado);
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
                } else
                {
                    String dataMessage = _functions.defaultMessage(table, "data");
                    return _functions.replyObject(dataMessage, false);
                }
            } else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
            
        }

        public TypeMessage Deletar(int id)
        {
            Estagio estagioProcurado = BuscarPorId(id);

            if (estagioProcurado != null)
            {
                try
                {
                    ctx.Estagio.Remove(estagioProcurado);
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
