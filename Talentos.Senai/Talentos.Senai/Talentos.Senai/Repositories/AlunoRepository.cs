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
    public class AlunoRepository : IAluno
    {
        private TalentosContext ctx = new TalentosContext();
        private readonly Functions _functions = new Functions();
        private readonly string table = "aluno";

        public List<Aluno> Listar() => ctx.Aluno.Include(a => a.IdEnderecoNavigation).Include(a => a.IdTipoUsuarioNavigation).ToList();

        public Aluno BuscarPorId(int id) => ctx.Aluno.FirstOrDefault(a => a.IdAluno == id);

        public TypeMessage Cadastrar(Aluno data)
        {
            Aluno alunoExistente = ctx.Aluno.FirstOrDefault(a => a.Email == data.Email || a.Rg == data.Rg || a.Cpf == data.Cpf);

            if(alunoExistente == null)
            {
                try
                {
                    ctx.Aluno.Add(data);
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
            else
            {
                string alunoExists = _functions.defaultMessage(table, "exists");
                return _functions.replyObject(alunoExists, false);
            }
        }

        public TypeMessage Atualizar(int id, Aluno dataAluno)
        {
            Aluno alunoParaAtualizar = BuscarPorId(id);

            if(alunoParaAtualizar != null)
            {
                try
                {
                    alunoParaAtualizar.Nome = dataAluno.Nome ?? alunoParaAtualizar.Nome;
                    alunoParaAtualizar.Email = dataAluno.Email ?? alunoParaAtualizar.Email;
                    alunoParaAtualizar.Senha = dataAluno.Senha ?? alunoParaAtualizar.Senha;
                    alunoParaAtualizar.NomeSocial = dataAluno.NomeSocial ?? alunoParaAtualizar.NomeSocial;
                    alunoParaAtualizar.Rg = dataAluno.Rg ?? alunoParaAtualizar.Rg;
                    alunoParaAtualizar.Cpf = dataAluno.Cpf ?? alunoParaAtualizar.Cpf;
                    alunoParaAtualizar.DataNascimento = dataAluno.DataNascimento != null ? dataAluno.DataNascimento : alunoParaAtualizar.DataNascimento;
                    alunoParaAtualizar.Genero = dataAluno.Genero ?? alunoParaAtualizar.Genero;
                    alunoParaAtualizar.CursoSenai = dataAluno.CursoSenai ??alunoParaAtualizar.CursoSenai;
                    alunoParaAtualizar.DataFormacao = dataAluno.DataFormacao  != null ? dataAluno.DataFormacao : alunoParaAtualizar.DataFormacao;
                    alunoParaAtualizar.Telefone = dataAluno.Telefone ?? alunoParaAtualizar.Telefone;
                    alunoParaAtualizar.TipoDefiencia = dataAluno.TipoDefiencia ?? alunoParaAtualizar.TipoDefiencia;
                    alunoParaAtualizar.DetalheDeficiencia = dataAluno.DetalheDeficiencia ?? alunoParaAtualizar.DetalheDeficiencia;
                    alunoParaAtualizar.PreferenciaArea = dataAluno.PreferenciaArea ?? alunoParaAtualizar.PreferenciaArea;
                    alunoParaAtualizar.Descricao = dataAluno.Descricao ?? alunoParaAtualizar.Descricao;
                    alunoParaAtualizar.Linkedin = dataAluno.Linkedin ?? alunoParaAtualizar.Linkedin;
                    alunoParaAtualizar.GitHub = dataAluno.GitHub ?? alunoParaAtualizar.GitHub;
                    alunoParaAtualizar.NomeFoto = dataAluno.NomeFoto ?? alunoParaAtualizar.NomeFoto;
                    alunoParaAtualizar.PerfilComportamental = dataAluno.PerfilComportamental ?? alunoParaAtualizar.PerfilComportamental;
                    alunoParaAtualizar.IdTipoUsuario = dataAluno.IdTipoUsuario ?? alunoParaAtualizar.IdTipoUsuario;
                    alunoParaAtualizar.IdEndereco = dataAluno.IdTipoUsuario ?? alunoParaAtualizar.IdEndereco;

                    ctx.Aluno.Update(alunoParaAtualizar);
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
            else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public TypeMessage Deletar(int id)
        {
            Aluno alunoParaDeletar = BuscarPorId(id);

            if(alunoParaDeletar != null)
            {
                try
                {
                    ctx.Aluno.Remove(alunoParaDeletar);
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
            else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }

    }
}
