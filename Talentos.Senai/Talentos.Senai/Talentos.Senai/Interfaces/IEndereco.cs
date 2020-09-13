using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IEndereco
    {   /// <summary>
        /// Lista todos os endereços
        /// </summary>
        /// <returns>Retorna uma lista de endereços</returns>
   
        List<Endereco> Listar();

        /// <summary>
        /// Cadastra um novo endereço
        /// </summary>
        /// <param name="novoEndereco">Objeto novoEndereco que será cadastrado</param>
        void CadastrarEndereco(Endereco novoEndereco);

        Endereco BuscarPorId(int id);

        /// <summary>
        /// Atualiza um endereco existente
        /// </summary>
        /// <param name="id">ID do endereço que será atualizado</param>
        /// <param name="enderecoAtualizado">Objeto enderecoAtualizado que será atualizado</param>
        void AtualizarEndereco(int id, Endereco enderecoAtualizado);

        /// <summary>
        /// Deleta um endereço existente
        /// </summary>
        /// <param name="id">ID do endereço que será deletado</param>
        void DeletarEndereco(int id);
    }
}
