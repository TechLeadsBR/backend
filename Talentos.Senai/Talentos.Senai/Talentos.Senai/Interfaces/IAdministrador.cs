using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IAdministrador
    {
        List<Administrador> Listar();

        Administrador BuscarPorId(int id);

        Administrador BuscarPorEmaileCpf(string email, string cpf);

        TypeMessage Cadastrar(Administrador data);

        TypeMessage Atualizar(int id, Administrador data);

        TypeMessage Deletar(int id);
    }
}
