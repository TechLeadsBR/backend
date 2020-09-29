using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class ExperienciaProfissional
    {
        public int IdExperienciaProfissional { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public DateTime DataInico { get; set; }
        public DateTime? DataFim { get; set; }
        public string Descricao { get; set; }
        public int? IdAluno { get; set; }

        public Aluno IdAlunoNavigation { get; set; }
    }
}
