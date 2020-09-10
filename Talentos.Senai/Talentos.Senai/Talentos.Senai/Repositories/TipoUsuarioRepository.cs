using System.Collections.Generic;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Domains;
using System.Linq;

namespace Talentos.Senai.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        TalentosContext ctx = new TalentosContext();

        public string Atualizar(TipoUsuario data, int id)
        {
            throw new System.NotImplementedException();
        }

        public string Cadastrar(TipoUsuario data)
        {
            throw new System.NotImplementedException();
        }

        public List<TipoUsuario> Listar()
        {
            return ctx.TipoUsuario.ToList();
        }

        public string Deletar(int id)
        {
            throw new System.NotImplementedException();
        }        
    }
}
