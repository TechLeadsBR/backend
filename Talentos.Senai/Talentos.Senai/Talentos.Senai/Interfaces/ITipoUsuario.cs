using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface ITipoUsuario
    {
        List<TipoUsuario> Listar();

        String Cadastrar(TipoUsuario data);

        TipoUsuario BuscarPorNome(String nome);

        String Atualizar(TipoUsuario data, int id);

        String Deletar(int id);
    }
}
