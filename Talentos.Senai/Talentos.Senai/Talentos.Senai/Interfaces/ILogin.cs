using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Interfaces
{
    interface ILogin
    {
        TypeMessage BuscarAluno(LoginViewModel data);

        TypeMessage BuscarEmpresa(LoginViewModel data);

        TypeMessage BuscarAdministrador(LoginViewModel data);

        object CreateToken(string email, string idUsuario, string tipoUsuario);
    }
}
