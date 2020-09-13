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
        private TalentosContext ctx = new TalentosContext();
        private readonly FunctionsGeneral _functions = new FunctionsGeneral();
        private readonly string table = "empresa";

        public TypeMessage Atualizar(int id, Empresa empresaAtualizado)
        {
            Empresa empresaParaAtualizar = BuscarPorId(id);

            if(empresaParaAtualizar != null)
            {
                try
                {
                    empresaParaAtualizar.Cnpj = empresaAtualizado.Cnpj ?? empresaParaAtualizar.Cnpj;
                    empresaParaAtualizar.RazaoSocial = empresaAtualizado.RazaoSocial ?? empresaParaAtualizar.RazaoSocial;
                    empresaParaAtualizar.Email = empresaAtualizado.Email ?? empresaParaAtualizar.Email;
                    empresaParaAtualizar.Senha = empresaAtualizado.Senha ?? empresaParaAtualizar.Senha;
                    empresaParaAtualizar.AtividadeEconomica = empresaAtualizado.AtividadeEconomica ?? empresaParaAtualizar.AtividadeEconomica;
                    empresaParaAtualizar.TelefoneDois = empresaAtualizado.TelefoneDois ?? empresaParaAtualizar.TelefoneDois;
                    empresaParaAtualizar.NomeFoto = empresaAtualizado.NomeFoto ?? empresaParaAtualizar.NomeFoto;
                    empresaParaAtualizar.IdTipoUsuario = empresaAtualizado.IdTipoUsuario ?? empresaParaAtualizar.IdTipoUsuario;
                    empresaParaAtualizar.DescricaoEmpresa = empresaAtualizado.DescricaoEmpresa ?? empresaParaAtualizar.DescricaoEmpresa;
                    empresaParaAtualizar.AtividadeEconomica = empresaAtualizado.AtividadeEconomica ?? empresaParaAtualizar.Telefone;

                    ctx.Empresa.Update(empresaParaAtualizar);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");

                    return _functions.replyObject(okMessage, true);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = _functions.defaultMessage(table, "error");
                    return _functions.replyObject(errorMessage, false);
                }
            }
            else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");

                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public Empresa BuscarPorId(int id) => ctx.Empresa.FirstOrDefault(e => e.IdEmpresa == id);

        /// <summary>
        /// Cadastra uma nova Empresa
        /// </summary>
        /// <param name="novoEmpresa">Objeto novoEmpresa que será cadastrada</param>
        public TypeMessage Cadastrar(Empresa novoEmpresa)
        {
            Empresa empresaExiste = ctx.Empresa.FirstOrDefault(e => e.Cnpj == novoEmpresa.Cnpj || e.Email == novoEmpresa.Email);

            if (empresaExiste == null)
            {
                try
                {
                    ctx.Empresa.Add(novoEmpresa);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");
                    return _functions.replyObject(okMessage, true);
                }catch(Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = _functions.defaultMessage(table, "error");
                    return _functions.replyObject(errorMessage, false);
                }
            }
            else
            {
                string notFoundMessage= _functions.defaultMessage(table, "exists");

                return _functions.replyObject(notFoundMessage, false);
            }
        }

        /// <summary>
        /// Deletar uma empresa
        /// </summary>
        /// <param name="id">ID da empresa que sera deleada</param>
        public TypeMessage Deletar(int id)
        {
            Empresa empresaBuscado = ctx.Empresa.Find(id);

            if (empresaBuscado != null)
            {
                try
                {
                    ctx.Empresa.Remove(empresaBuscado);           
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");

                    return _functions.replyObject(okMessage, true);
                } catch(Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = _functions.defaultMessage(table, "error");
                    return _functions.replyObject(errorMessage, false);
                }
            }
            else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");

                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public List<Empresa> Listar() => ctx.Empresa.ToList();
    }
}
