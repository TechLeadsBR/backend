using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;

namespace Talentos.Senai.Repositories
{
    public class EnderecoRepository : IEndereco
    {
        TalentosContext ctx = new TalentosContext();

        public void AtualizarEndereco(int id, Endereco enderecoAtualizado)
        {
            Endereco enderecoAtualizar = ctx.Endereco.Find(id);
 
                enderecoAtualizar.Cep = enderecoAtualizado.Cep != null ? enderecoAtualizado.Cep : enderecoAtualizar.Cep;
                enderecoAtualizar.Logradouro = enderecoAtualizado.Logradouro != null ? enderecoAtualizado.Logradouro : enderecoAtualizar.Logradouro;
                enderecoAtualizar.Bairro = enderecoAtualizado.Bairro != null ? enderecoAtualizado.Bairro : enderecoAtualizar.Bairro;
                enderecoAtualizar.Numero = enderecoAtualizado.Numero != null ? enderecoAtualizado.Numero : enderecoAtualizar.Numero;
                enderecoAtualizar.Complemento = enderecoAtualizado.Complemento != null ? enderecoAtualizado.Complemento : enderecoAtualizar.Complemento;
                enderecoAtualizar.Localidade = enderecoAtualizado.Localidade != null ? enderecoAtualizado.Localidade : enderecoAtualizar.Localidade;

            ctx.Endereco.Update(enderecoAtualizar);
            ctx.SaveChanges();
        }

      

        public Endereco BuscarPorId(int id)
        {
            return ctx.Endereco.FirstOrDefault(e => e.IdEndereco == id);
        }

        public void CadastrarEndereco(Endereco novoEndereco)
        {
            ctx.Endereco.Add(novoEndereco);

            ctx.SaveChanges();

        }

        public void DeletarEndereco(int id)
        {
            Endereco enderecoBuscar = ctx.Endereco.Find(id);
            ctx.Endereco.Remove(enderecoBuscar);
        }

        public List<Endereco> Listar()
        {
            return ctx.Endereco.ToList();
        }
    }
}
