using System;
using System.Collections.Generic;

namespace Talentos.Senai.Domains
{
    public partial class Aluno
    {
        public Aluno()
        {
            Estagio = new HashSet<Estagio>();
            ExperienciaProfissional = new HashSet<ExperienciaProfissional>();
            FormacaoAcademica = new HashSet<FormacaoAcademica>();
            Idioma = new HashSet<Idioma>();
            InscricaoEmprego = new HashSet<InscricaoEmprego>();
        }

        public int IdAluno { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NomeSocial { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public string CursoSenai { get; set; }
        public DateTime DataFormacao { get; set; }
        public string Telefone { get; set; }
        public string TipoDefiencia { get; set; }
        public string DetalheDeficiencia { get; set; }
        public string PreferenciaArea { get; set; }
        public string Descricao { get; set; }
        public string Linkedin { get; set; }
        public string GitHub { get; set; }
        public string NomeFoto { get; set; }
        public string PerfilComportamental { get; set; }
        public int? IdTipoUsuario { get; set; }
        public int? IdEndereco { get; set; }

        public Endereco IdEnderecoNavigation { get; set; }
        public TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public ICollection<Estagio> Estagio { get; set; }
        public ICollection<ExperienciaProfissional> ExperienciaProfissional { get; set; }
        public ICollection<FormacaoAcademica> FormacaoAcademica { get; set; }
        public ICollection<Idioma> Idioma { get; set; }
        public ICollection<InscricaoEmprego> InscricaoEmprego { get; set; }
    }
}
