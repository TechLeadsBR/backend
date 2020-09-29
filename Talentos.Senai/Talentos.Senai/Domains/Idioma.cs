using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class Idioma
    {
        public int IdIdioma { get; set; }
        public string Idioma1 { get; set; }
        public string Nivel { get; set; }
        public int? IdAluno { get; set; }

        public Aluno IdAlunoNavigation { get; set; }
    }
}
