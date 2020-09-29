using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class Estagio
    {
        public int IdEstagio { get; set; }
        public string Responsavel { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public string StatusContrato { get; set; }
        public bool? Documentos { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdAluno { get; set; }

        public Aluno IdAlunoNavigation { get; set; }
        public Empresa IdEmpresaNavigation { get; set; }
    }
}
