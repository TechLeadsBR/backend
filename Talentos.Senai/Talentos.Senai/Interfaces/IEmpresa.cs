using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    /// <summary>
    /// Interface responsável pelo EmpresaRepository
    /// </summary>
    /// 
    interface IEmpresa
    {
        /// <summary>
        /// Lista todos as Empresas
        /// </summary>
        /// <returns>Uma lista de Empresas</returns>
        List<Empresa> Listar();

        /// <summary>
        /// Cadastra uma nova Empresa
        /// </summary>
        /// <param name="novoEmpresa">Objeto novoEmpresa que será cadastrado</param>
        TypeMessage Cadastrar(Empresa novoEmpresa);

        Empresa BuscarPorId(int id, bool allData= true);

        /// <summary>
        /// Atualiza uma Empresa existente
        /// </summary>
        /// <param name="id">ID da Empresa que será atualizado</param>
        /// <param name="empresaAtualizado">Objeto com as novas informações</param>
        TypeMessage Atualizar(int id, Empresa empresaAtualizado);

        /// <summary>
        /// Deleta uma Empresa existente
        /// </summary>
        /// <param name="id">ID da Empresa que será deletado</param>
        TypeMessage Deletar(int id);

    }
}
