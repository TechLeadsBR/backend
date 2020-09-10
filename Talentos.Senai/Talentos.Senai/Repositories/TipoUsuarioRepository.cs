using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Contexts;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;

namespace Talentos.Senai.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        Contexts.Talentos ctx = new Contexts.Talentos();

        public List<TipoUsuario> Listar()
        {
            return ctx.TipoUsuario.ToList();
        }

        public string Cadastrar(TipoUsuario data)
        {
            throw new NotImplementedException();
        }

        public string Atualizar(TipoUsuario data, int id)
        {
            throw new NotImplementedException();
        }

        public string Deletar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
