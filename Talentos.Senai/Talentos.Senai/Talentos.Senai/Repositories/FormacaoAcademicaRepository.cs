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
    public class FormacaoAcademicaRepository : IFormacaoAcademica
    {
        private TalentosContext ctx = new TalentosContext();
        private readonly Functions _functions = new Functions();
        private readonly string table = "formacaoAcademica";
  
        public List<FormacaoAcademica> Listar() => ctx.FormacaoAcademica.Include(f => f.IdAlunoNavigation).ToList();

        public FormacaoAcademica BuscarPorId(int id) => ctx.FormacaoAcademica.FirstOrDefault(f => f.IdFormacaoAcademica == id);

        public TypeMessage Cadastrar(FormacaoAcademica data)
        {
            FormacaoAcademica formacaoExistente = ctx.FormacaoAcademica.FirstOrDefault(f => f.IdFormacaoAcademica == data.IdFormacaoAcademica);

            if (formacaoExistente == null)
            {
                try
                {
                    ctx.FormacaoAcademica.Add(data);
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

        public TypeMessage Atualizar(int id, FormacaoAcademica dataFormacao)
        {
            FormacaoAcademica formacaoParaAtualizar = BuscarPorId(id);

            if (formacaoParaAtualizar != null)
            {
                try
                {
                    formacaoParaAtualizar.NomeCurso = dataFormacao.NomeCurso ?? formacaoParaAtualizar.NomeCurso ;
                    formacaoParaAtualizar.Instituicao = dataFormacao.Instituicao ?? formacaoParaAtualizar.Instituicao;
                    formacaoParaAtualizar.TipoCurso = dataFormacao.TipoCurso ?? formacaoParaAtualizar.TipoCurso;
                    formacaoParaAtualizar.InicioCurso = dataFormacao.InicioCurso != null ? dataFormacao.InicioCurso : formacaoParaAtualizar.InicioCurso;
                    formacaoParaAtualizar.TerminoCurso = dataFormacao.TerminoCurso != null ? dataFormacao.TerminoCurso : formacaoParaAtualizar.TerminoCurso;
                    formacaoParaAtualizar.IdAluno = dataFormacao.IdAluno ?? formacaoParaAtualizar.IdAluno;
                    
                    ctx.FormacaoAcademica.Update(formacaoParaAtualizar);
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
            FormacaoAcademica formacaoParaDeletar = BuscarPorId(id);

            if (formacaoParaDeletar != null)
            {
                try
                {
                    ctx.FormacaoAcademica.Remove(formacaoParaDeletar);
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
