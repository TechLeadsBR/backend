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
        private TalentosContext ctx = new TalentosContext();
        private readonly Functions _functions = new Functions();
        private readonly string table = "inscricaoemprego";
        private readonly IAluno _alunoRepository = new AlunoRepository();
        private readonly IVagaEmprego _vagaEmpregoRepository = new VagaEmpregoRepository();

        public List<InscricaoEmprego> Listar() => ctx.InscricaoEmprego.Include(e => e.IdAlunoNavigation).Include(e => e.IdVagaEmpregoNavigation).ToList();
        public InscricaoEmprego BuscarporIdAlunoeVagaEmprego(int idAluno, int idVagaEmprego) => ctx.InscricaoEmprego.FirstOrDefault(e => e.IdAluno == idAluno && e.IdVagaEmprego == idVagaEmprego);
        public InscricaoEmprego BuscarporIdInscricao(int id) => ctx.InscricaoEmprego.FirstOrDefault(e => e.IdInscricaoEmprego == id);

        public TypeMessage Cadastrar(InscricaoEmprego data)
        {
            InscricaoEmprego inscricaoProcurada = BuscarporIdAlunoeVagaEmprego(
                data.IdAluno.GetValueOrDefault(), 
                data.IdVagaEmprego.GetValueOrDefault());
            VagaEmprego vagaBuscada = _vagaEmpregoRepository.BuscarPorId(data.IdVagaEmprego.GetValueOrDefault());
            Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());


            if (inscricaoProcurada == null)
            {
                if(vagaBuscada != null && alunoBuscado != null)
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
                } else
                {
                    string dataMessage = _functions.defaultMessage(table, "data");
                    return _functions.replyObject(dataMessage, false);
                }

            } else {
                string existsMessage = _functions.defaultMessage(table, "exists");
                return _functions.replyObject(existsMessage, false);
            }
        }


        public TypeMessage Atualizar(int id, InscricaoEmprego data)
        {
            InscricaoEmprego inscricaoBuscada = BuscarporIdInscricao(id);

            if (inscricaoBuscada != null)
            {
                Aluno alunoBuscado = _alunoRepository.BuscarPorId(data.IdAluno.GetValueOrDefault());
                VagaEmprego vagaBuscada = _vagaEmpregoRepository.BuscarPorId(data.IdVagaEmprego.GetValueOrDefault());
                InscricaoEmprego inscricaoExistente = BuscarporIdAlunoeVagaEmprego(
                    data.IdAluno.GetValueOrDefault(), data.IdVagaEmprego.GetValueOrDefault());

                if(alunoBuscado != null && vagaBuscada != null && inscricaoExistente == null)
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

        

        public TypeMessage Deletar(int id)
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
