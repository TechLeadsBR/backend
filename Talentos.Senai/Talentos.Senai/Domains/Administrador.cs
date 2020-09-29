using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class Administrador
    {
        public int IdAdministrador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public int? IdTipoUsuario { get; set; }

        public TipoUsuario IdTipoUsuarioNavigation { get; set; }
    }
}
