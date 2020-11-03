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
    public class InscricaoEmpregoRepository : IInscricaoEmprego
    {
        private readonly Functions _functions;
        private readonly IAluno _alunoRepository;
        private readonly IVagaEmprego _vagaEmpregoRepository;
        private readonly string table;

        public InscricaoEmpregoRepository()
        {
            _functions = new Functions();
            _alunoRepository = new AlunoRepository();
            _vagaEmpregoRepository = new VagaEmpregoRepository();
            table = "inscricaoemprego";

        }

        public List<InscricaoEmprego> Listar(int jti, string roleUser)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                if(roleUser == Users.Administrator)
                {
                    return ctx.InscricaoEmprego
                        .Include(a => a.IdVagaEmpregoNavigation.IdEmpresaNavigation)
                        .ToList();
                }else
                {
                    return ctx.InscricaoEmprego
                        .Include(a => a.IdVagaEmpregoNavigation.IdEmpresaNavigation)
                        .Where(a => a.IdAluno == jti)
                        .ToList();
                }
            }
        }

        public InscricaoEmprego BuscarporIdAlunoeVagaEmprego(int idAluno, int idVagaEmprego)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.InscricaoEmprego
                    .FirstOrDefault(e => e.IdAluno == idAluno && e.IdVagaEmprego == idVagaEmprego);
            }
        }
        public InscricaoEmprego BuscarporIdInscricao(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.InscricaoEmprego.FirstOrDefault(e => e.IdInscricaoEmprego == id);
            }
        }

        public TypeMessage Cadastrar(InscricaoEmprego data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                InscricaoEmprego inscricaoProcurada = BuscarporIdAlunoeVagaEmprego(
                data.IdAluno.GetValueOrDefault(),
                data.IdVagaEmprego.GetValueOrDefault());


                if (inscricaoProcurada == null)
                {
                    VagaEmprego vagaBuscada = _vagaEmpregoRepository.BuscarPorId(data.IdVagaEmprego.GetValueOrDefault());
                    Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());

                    if (vagaBuscada != null && alunoBuscado != null)
                    {
                        try
                        {
                            ctx.InscricaoEmprego.Add(data);
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
                        string dataMessage = _functions.defaultMessage(table, "data");
                        return _functions.replyObject(dataMessage, false);
                    }

                }
                else
                {
                    string existsMessage = _functions.defaultMessage(table, "exists");
                    return _functions.replyObject(existsMessage, false);
                }
            }
        }


        public TypeMessage Atualizar(int id, InscricaoEmprego data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                InscricaoEmprego inscricaoBuscada = BuscarporIdInscricao(id);

                if (inscricaoBuscada != null)
                {
                    InscricaoEmprego inscricaoExistente = BuscarporIdAlunoeVagaEmprego(
                        data.IdAluno.GetValueOrDefault(), data.IdVagaEmprego.GetValueOrDefault());
                    Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());
                    VagaEmprego vagaBuscada = _vagaEmpregoRepository.BuscarPorId(data.IdVagaEmprego.GetValueOrDefault());

                    if (alunoBuscado != null && vagaBuscada != null && inscricaoExistente == null)
                    {
                        try
                        {
                            inscricaoBuscada.DataInscricao = data.DataInscricao != null ? data.DataInscricao : inscricaoBuscada.DataInscricao;
                            inscricaoBuscada.IdAluno = data.IdAluno ?? inscricaoBuscada.IdAluno;
                            inscricaoBuscada.IdVagaEmprego = data.IdVagaEmprego ?? inscricaoBuscada.IdVagaEmprego;


                            ctx.InscricaoEmprego.Update(inscricaoBuscada);
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
                        string dataMessage = _functions.defaultMessage(table, "data");
                        return _functions.replyObject(dataMessage, false);
                    }
                }
                else
                {
                    string dataMessage = _functions.defaultMessage(table, "data");
                    return _functions.replyObject(dataMessage, false);
                }
            }
        }



        public TypeMessage Deletar(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                InscricaoEmprego inscricaoBuscada = BuscarporIdInscricao(id);

                if (inscricaoBuscada != null)
                {
                    try
                    {
                        ctx.InscricaoEmprego.Remove(inscricaoBuscada);
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
