using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Talentos.Senai.Domains
{
    public partial class TalentosContext : DbContext
    {
        public TalentosContext()
        {
        }

        public TalentosContext(DbContextOptions<TalentosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Estagio> Estagio { get; set; }
        public virtual DbSet<ExperienciaProfissional> ExperienciaProfissional { get; set; }
        public virtual DbSet<FormacaoAcademica> FormacaoAcademica { get; set; }
        public virtual DbSet<Idioma> Idioma { get; set; }
        public virtual DbSet<InscricaoEmprego> InscricaoEmprego { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<VagaEmprego> VagaEmprego { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string database = "DESKTOP-G005E3N\\SQLEXPRESS";

                optionsBuilder.UseSqlServer($"Data Source={database}; Initial Catalog=Talentos; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdministrador);

                entity.HasIndex(e => e.Cpf)
                    .HasName("UQ__Administ__C1F897319925AAAD")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Administ__A9D10534684DD6FA")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Administrador)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Administr__IdTip__2B3F6F97");
            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.IdAluno);

                entity.HasIndex(e => e.Cpf)
                    .HasName("UQ__Aluno__C1F8973170722AFA")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Aluno__A9D1053420A05467")
                    .IsUnique();

                entity.HasIndex(e => e.Rg)
                    .HasName("UQ__Aluno__321537C82A9186F4")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CursoSenai)
                    .IsRequired()
                    .HasColumnName("CursoSENAI")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DataFormacao).HasColumnType("date");

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Descricao).HasColumnType("text");

                entity.Property(e => e.DetalheDeficiencia).HasColumnType("text");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GitHub)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Linkedin)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NomeFoto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NomeSocial)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PerfilComportamental)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenciaArea)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasColumnName("RG")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDefiencia)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.Aluno)
                    .HasForeignKey(d => d.IdEndereco)
                    .HasConstraintName("FK__Aluno__IdEnderec__36B12243");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Aluno)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Aluno__IdTipoUsu__35BCFE0A");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Empresa__AA57D6B489E8D27B")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Empresa__A9D105349EACE280")
                    .IsUnique();

                entity.Property(e => e.AtividadeEconomica)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DescricaoEmpresa)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NomeFoto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TelefoneDois)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Empresa)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Empresa__IdTipoU__300424B4");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.IdEndereco);

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Complemento)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Localidade)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estagio>(entity =>
            {
                entity.HasKey(e => e.IdEstagio);

                entity.Property(e => e.Documentos).HasDefaultValueSql("((0))");

                entity.Property(e => e.Inicio).HasColumnType("date");

                entity.Property(e => e.Responsavel)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StatusContrato)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Termino).HasColumnType("date");

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.Estagio)
                    .HasForeignKey(d => d.IdAluno)
                    .HasConstraintName("FK__Estagio__IdAluno__412EB0B6");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Estagio)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__Estagio__IdEmpre__403A8C7D");
            });

            modelBuilder.Entity<ExperienciaProfissional>(entity =>
            {
                entity.HasKey(e => e.IdExperienciaProfissional);

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DataFim).HasColumnType("date");

                entity.Property(e => e.DataInico).HasColumnType("date");

                entity.Property(e => e.Descricao).HasColumnType("text");

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.ExperienciaProfissional)
                    .HasForeignKey(d => d.IdAluno)
                    .HasConstraintName("FK__Experienc__IdAlu__47DBAE45");
            });

            modelBuilder.Entity<FormacaoAcademica>(entity =>
            {
                entity.HasKey(e => e.IdFormacaoAcademica);

                entity.Property(e => e.InicioCurso).HasColumnType("date");

                entity.Property(e => e.Instituicao)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NomeCurso)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TerminoCurso).HasColumnType("date");

                entity.Property(e => e.TipoCurso)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.FormacaoAcademica)
                    .HasForeignKey(d => d.IdAluno)
                    .HasConstraintName("FK__FormacaoA__IdAlu__4AB81AF0");
            });

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.HasKey(e => e.IdIdioma);

                entity.Property(e => e.Idioma1)
                    .HasColumnName("Idioma")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nivel)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.Idioma)
                    .HasForeignKey(d => d.IdAluno)
                    .HasConstraintName("FK__Idioma__IdAluno__398D8EEE");
            });

            modelBuilder.Entity<InscricaoEmprego>(entity =>
            {
                entity.HasKey(e => e.IdInscricaoEmprego);

                entity.Property(e => e.DataInscricao).HasColumnType("date");

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.InscricaoEmprego)
                    .HasForeignKey(d => d.IdAluno)
                    .HasConstraintName("FK__Inscricao__IdAlu__440B1D61");

                entity.HasOne(d => d.IdVagaEmpregoNavigation)
                    .WithMany(p => p.InscricaoEmprego)
                    .HasForeignKey(d => d.IdVagaEmprego)
                    .HasConstraintName("FK__Inscricao__IdVag__44FF419A");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.HasIndex(e => e.TituloTipoUsuario)
                    .HasName("UQ__TipoUsua__37BBE07E5F7598F6")
                    .IsUnique();

                entity.Property(e => e.TituloTipoUsuario)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VagaEmprego>(entity =>
            {
                entity.HasKey(e => e.IdVagaEmprego);

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DescricaoVaga)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Habilidade)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Nivel)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RemuneracaoBeneficio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoContrato)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.VagaEmprego)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__VagaEmpre__IdEmp__3C69FB99");
            });
        }
    }
}
