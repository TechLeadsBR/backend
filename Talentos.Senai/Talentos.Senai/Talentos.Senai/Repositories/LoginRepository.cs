using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.General;
using Talentos.Senai.Interfaces;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Repositories
{
    public class LoginRepository : ILogin
    {
        private TalentosContext ctx = new TalentosContext();

        public Aluno BuscarAluno(LoginViewModel login) => ctx.Aluno.FirstOrDefault(a => a.Email == login.Email && a.Senha == login.Senha);

        public Empresa BuscarEmpresa(LoginViewModel login) => ctx.Empresa.FirstOrDefault(e => e.Email == login.Email && e.Senha == login.Senha);

        public Usuario Usuario(LoginViewModel login)
        {
            return new Usuario
            {
                aluno = BuscarAluno(login) ?? null,
                empresa = BuscarEmpresa(login) ?? null
            };
        }
    }
}
