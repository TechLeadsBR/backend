using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;

namespace Talentos.Senai.Repositories
{
    public class EmpresaRepository : IEmpresa
    {
        TalentosContext ctx = new TalentosContext();
        public bool Atualizar(int id, Empresa empresaAtualizado)
        {
            try
            {
                Empresa empresaParaAtualizar = BuscarPorId(id);

                empresaParaAtualizar.Cnpj = empresaAtualizado.Cnpj != null ? empresaAtualizado.Cnpj : empresaParaAtualizar.Cnpj;
                empresaParaAtualizar.RazaoSocial = empresaAtualizado.RazaoSocial != null ? empresaAtualizado.RazaoSocial : empresaParaAtualizar.RazaoSocial;
                empresaParaAtualizar.Email = empresaAtualizado.Email != null ? empresaAtualizado.Email : empresaParaAtualizar.Email;
                empresaParaAtualizar.Senha = empresaAtualizado.Senha != null ? empresaAtualizado.Senha : empresaParaAtualizar.Senha;
                empresaParaAtualizar.AtividadeEconomica = empresaAtualizado.AtividadeEconomica != null ? empresaAtualizado.AtividadeEconomica : empresaParaAtualizar.AtividadeEconomica;
                empresaParaAtualizar.TelefoneDois = empresaAtualizado.TelefoneDois != null ? empresaAtualizado.TelefoneDois : empresaParaAtualizar.TelefoneDois;
                empresaParaAtualizar.NomeFoto = empresaAtualizado.NomeFoto != null ? empresaAtualizado.NomeFoto : empresaParaAtualizar.NomeFoto;
                empresaParaAtualizar.IdTipoUsuario = empresaAtualizado.IdTipoUsuario != null ? empresaAtualizado.IdTipoUsuario : empresaParaAtualizar.IdTipoUsuario;
                empresaParaAtualizar.DescricaoEmpresa = empresaAtualizado.DescricaoEmpresa != null ? empresaAtualizado.DescricaoEmpresa : empresaParaAtualizar.DescricaoEmpresa;
                empresaParaAtualizar.AtividadeEconomica = empresaAtualizado.AtividadeEconomica != null ? empresaAtualizado.Telefone : empresaParaAtualizar.Telefone;

                ctx.Empresa.Update(empresaParaAtualizar);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
                return false;
            }
        }

        public Empresa BuscarPorId(int id) => ctx.Empresa.FirstOrDefault(e => e.IdEmpresa == id);

        /// <summary>
        /// Cadastra uma nova Empresa
        /// </summary>
        /// <param name="novoEmpresa">Objeto novoEmpresa que será cadastrada</param>
        public bool Cadastrar(Empresa novoEmpresa)
        {
            Empresa empresaExiste = ctx.Empresa.FirstOrDefault(e => e.Cnpj == novoEmpresa.Cnpj || e.Email == novoEmpresa.Email);

            if (empresaExiste == null)
            {
                // Adiciona este novoEmpresa
                ctx.Empresa.Add(novoEmpresa);

                // Salva as informações para serem gravadas no banco de dados
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletar uma empresa
        /// </summary>
        /// <param name="id">ID da empresa que sera deleada</param>
        public bool Deletar(int id)
        {
            Empresa empresaBuscado = ctx.Empresa.Find(id);

            if (empresaBuscado != null)
            {
                ctx.Empresa.Remove(empresaBuscado);           
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Empresa> Listar() => ctx.Empresa.ToList();
    }
}
