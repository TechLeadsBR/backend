using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class Endereco
    {
        public Endereco()
        {
            Aluno = new HashSet<Aluno>();
        }

        public int IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Localidade { get; set; }

        public virtual ICollection<Aluno> Aluno { get; set; }
    }
}
