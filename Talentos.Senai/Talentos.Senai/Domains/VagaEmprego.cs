using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class VagaEmprego
    {
        public VagaEmprego()
        {
            InscricaoEmprego = new HashSet<InscricaoEmprego>();
        }

        public int IdVagaEmprego { get; set; }
        public string Titulo { get; set; }
        public string Nivel { get; set; }
        public string Cidade { get; set; }
        public string DescricaoVaga { get; set; }
        public string Habilidade { get; set; }
        public string RemuneracaoBeneficio { get; set; }
        public string TipoContrato { get; set; }
        public int? IdEmpresa { get; set; }

        public Empresa IdEmpresaNavigation { get; set; }
        public ICollection<InscricaoEmprego> InscricaoEmprego { get; set; }
    }
}
