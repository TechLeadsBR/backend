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
        TipoUsuario BuscarPorNome(String titulo);

        TipoUsuario BuscarPorId(int id);

        TypeMessage Cadastrar(TipoUsuario data);
        
        TypeMessage Atualizar(TipoUsuario tituloNovo, int id);

        TypeMessage Deletar(int id);
    }
}
