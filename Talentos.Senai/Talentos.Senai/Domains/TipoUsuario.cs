using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Administrador = new HashSet<Administrador>();
            Aluno = new HashSet<Aluno>();
            Empresa = new HashSet<Empresa>();
        }

        public int IdTipoUsuario { get; set; }
        public string TituloTipoUsuario { get; set; }

        public virtual ICollection<Administrador> Administrador { get; set; }
        public virtual ICollection<Aluno> Aluno { get; set; }
        public virtual ICollection<Empresa> Empresa { get; set; }
    }
}
