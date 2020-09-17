using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.General;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Interfaces
{
    interface ILogin
    {
        Aluno BuscarAluno(LoginViewModel data);

        Empresa BuscarEmpresa(LoginViewModel data);

        Usuario BuscarUsuario(LoginViewModel data);

        string CreateToken(string email, string tipoUsuario);
    }
}
