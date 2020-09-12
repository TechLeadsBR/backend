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
        private string table;

        public AlunoRepository()
        {
            _functions = new General();
            table = "aluno";
        }

        public List<Aluno> Listar() => ctx.Aluno.ToList();

        public Aluno BuscarPorId(int id) => ctx.Aluno.FirstOrDefault(a => a.IdAluno == id);

        public TypeMessage Cadastrar(Aluno data)
        {
            Aluno alunoExistente = ctx.Aluno.FirstOrDefault(a => a.Email == data.Email || a.Rg == data.Rg || a.Cpf == data.Cpf);
            if(alunoExistente == null)
            {
                ctx.Aluno.Add(data);
                ctx.SaveChanges();

                string okMessage = _functions.defaultMessage(table, data.IdAluno, "ok");

                return _functions.returnResponse(okMessage, true);
            }else
            {
                string alunoExists = _functions.defaultMessage(table, data.IdAluno, "existente");

                return _functions.returnResponse(alunoExists, false);
            }
        }

        public TypeMessage Atualizar(int id, Aluno dataAluno)
        {
            Aluno alunoParaAtualizar = BuscarPorId(id);

            if(alunoParaAtualizar != null)
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

                string okMessage = _functions.defaultMessage(table, id, "ok");
                return _functions.returnResponse(okMessage, true);

            } else
            {
                string notFoundMessage = _functions.defaultMessage(table, id, "notfound");
                return _functions.returnResponse(notFoundMessage, false);
            }
        }

        public TypeMessage Deletar(int id)
        {
            Aluno alunoParaDeletar = BuscarPorId(id);

            if(alunoParaDeletar != null)
            {
                ctx.Aluno.Remove(alunoParaDeletar);
                ctx.SaveChanges();

                string okMessage = _functions.defaultMessage(table, id, "ok");
                return _functions.returnResponse(okMessage, true);
            } else
            {
                string notFoundMessage = _functions.defaultMessage(table, id, "notfound");
                return _functions.returnResponse(notFoundMessage, false);
            }
        }

    }
}
