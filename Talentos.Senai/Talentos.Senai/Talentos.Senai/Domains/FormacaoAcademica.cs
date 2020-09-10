using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class FormacaoAcademica
    {
        public int IdFormacaoAcademica { get; set; }
        public string NomeCurso { get; set; }
        public string Instituicao { get; set; }
        public string TipoCurso { get; set; }
        public DateTime InicioCurso { get; set; }
        public DateTime? TerminoCurso { get; set; }
        public int? IdAluno { get; set; }

        public Aluno IdAlunoNavigation { get; set; }
    }
}
