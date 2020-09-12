using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class EmpresaRepository : IEmpresa
    {
        TalentosContext ctx = new TalentosContext();

        private General _function;

        public EmpresaRepository()
        {
            _function = new General();
        }

        public TypeMessage Atualizar(int id, Empresa empresaAtualizado)
        {
            string table = "empresa";

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

                string okMessage = _function.defaultMessage(table, id, "ok");

                return _function.returnResponse(okMessage, true);
            }
            catch(Exception error)
            {
                string errorMessage = _function.defaultMessage(table, id, "error");

                return _function.returnResponse(errorMessage, false);
            }
        }

        public Empresa BuscarPorId(int id) => ctx.Empresa.FirstOrDefault(e => e.IdEmpresa == id);

        /// <summary>
        /// Cadastra uma nova Empresa
        /// </summary>
        /// <param name="novoEmpresa">Objeto novoEmpresa que será cadastrada</param>
        public TypeMessage Cadastrar(Empresa novoEmpresa)
        {
            string table = "empresa";
            int id = novoEmpresa.IdEmpresa;

            Empresa empresaExiste = ctx.Empresa.FirstOrDefault(e => e.Cnpj == novoEmpresa.Cnpj || e.Email == novoEmpresa.Email);

            if (empresaExiste == null)
            {
                ctx.Empresa.Add(novoEmpresa);

                ctx.SaveChanges();

                string okMessage = _function.defaultMessage(table, id, "ok");
                return _function.returnResponse(okMessage, true);
            }
            else
            {
                string existsMessage = _function.defaultMessage(table, id, "existente");

                return _function.returnResponse(existsMessage, false);
            }
        }

        /// <summary>
        /// Deletar uma empresa
        /// </summary>
        /// <param name="id">ID da empresa que sera deleada</param>
        public TypeMessage Deletar(int id)
        {
            Empresa empresaBuscado = ctx.Empresa.Find(id);
            string table = "empresa";

            if (empresaBuscado != null)
            {
                ctx.Empresa.Remove(empresaBuscado);           
                ctx.SaveChanges();

                string okMessage = _function.defaultMessage(table, id, "ok");

                return _function.returnResponse(okMessage, true);
            }
            else
            {
                string errorMessage = _function.defaultMessage(table, id, "error");

                return _function.returnResponse(errorMessage, false);
            }
        }

        public List<Empresa> Listar() => ctx.Empresa.ToList();
    }
}
