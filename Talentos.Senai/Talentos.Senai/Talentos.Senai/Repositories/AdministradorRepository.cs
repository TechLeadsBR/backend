using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class AdministradorRepository : IAdministrador
    {
        TalentosContext ctx = new TalentosContext();

        private string table;
        private General _functions;

        public AdministradorRepository()
        {
            _functions = new General();
            table = "administrador";
        }

        public List<Administrador> Listar() => ctx.Administrador.ToList();

        public Administrador BuscarPorId(int id) => ctx.Administrador.FirstOrDefault(a => a.IdAdministrador == id);

        public TypeMessage Cadastrar(Administrador data)
        {
            throw new NotImplementedException();
        }

        public TypeMessage Atualizar(int id, Administrador data)
        {
            throw new NotImplementedException();
        }

        public TypeMessage Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
