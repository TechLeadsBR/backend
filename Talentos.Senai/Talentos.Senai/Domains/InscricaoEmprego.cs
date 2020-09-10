using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class InscricaoEmprego
    {
        public int IdInscricaoEmprego { get; set; }
        public DateTime DataInscricao { get; set; }
        public int? IdAluno { get; set; }
        public int? IdVagaEmprego { get; set; }

        public virtual Aluno IdAlunoNavigation { get; set; }
        public virtual VagaEmprego IdVagaEmpregoNavigation { get; set; }
    }
}
