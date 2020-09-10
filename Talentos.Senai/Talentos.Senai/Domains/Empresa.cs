using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class Empresa
    {
        public Empresa()
        {
            Estagio = new HashSet<Estagio>();
            VagaEmprego = new HashSet<VagaEmprego>();
        }

        public int IdEmpresa { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cnpj { get; set; }
        public string AtividadeEconomica { get; set; }
        public string Telefone { get; set; }
        public string TelefoneDois { get; set; }
        public string NomeFoto { get; set; }
        public string DescricaoEmpresa { get; set; }
        public int? IdTipoUsuario { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Estagio> Estagio { get; set; }
        public virtual ICollection<VagaEmprego> VagaEmprego { get; set; }
    }
}
