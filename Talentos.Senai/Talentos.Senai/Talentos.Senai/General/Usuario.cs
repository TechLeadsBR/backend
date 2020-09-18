using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.General
{
    public class Usuario
    {
        public Aluno aluno { get; set; }

        public Empresa empresa { get; set; }
    }
}
