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
        Aluno BuscarAluno(LoginViewModel login);

        Empresa BuscarEmpresa(LoginViewModel login);

        Usuario Usuario(LoginViewModel login);
    }
}
