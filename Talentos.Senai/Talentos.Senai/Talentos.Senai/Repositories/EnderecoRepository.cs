using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class EnderecoRepository : IEndereco
    {
        private TalentosContext ctx = new TalentosContext();
        private readonly FunctionsGeneral _functions = new FunctionsGeneral();
        private readonly string table = "endereco";

        public List<Endereco> Listar() => ctx.Endereco.ToList();

        public Endereco BuscarPorId(int id) => ctx.Endereco.FirstOrDefault(e => e.IdEndereco == id);

        public TypeMessage AtualizarEndereco(int id, Endereco data)
        {
            Endereco enderecoAtualizar = BuscarPorId(id);

            if(enderecoAtualizar != null)
            {
                try
                {
                    enderecoAtualizar.Cep = data.Cep ?? enderecoAtualizar.Cep;
                    enderecoAtualizar.Logradouro = data.Logradouro ?? enderecoAtualizar.Logradouro;
                    enderecoAtualizar.Bairro = data.Bairro ?? enderecoAtualizar.Bairro;
                    enderecoAtualizar.Numero = data.Numero ?? enderecoAtualizar.Numero;
                    enderecoAtualizar.Complemento = data.Complemento ?? enderecoAtualizar.Complemento;
                    enderecoAtualizar.Localidade = data.Localidade ?? enderecoAtualizar.Localidade;

                    ctx.Endereco.Update(enderecoAtualizar);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");
                    return _functions.replyObject(okMessage, true);
                } catch(Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = _functions.defaultMessage(table, "error");
                    return _functions.replyObject(errorMessage, false);
                }
            } else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public TypeMessage CadastrarEndereco(Endereco novoEndereco)
        {
            if(novoEndereco != null)
            {
                try
                {
                    ctx.Endereco.Add(novoEndereco);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");
                    return _functions.replyObject(okMessage, true);
                } catch(Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = _functions.defaultMessage(table, "error");
                    return _functions.replyObject(errorMessage, false);
                }
            } else
            {
                string errorMessage = _functions.defaultMessage(table, "error");
                return _functions.replyObject(errorMessage, false);
            }
        }

        public TypeMessage DeletarEndereco(int id)
        {
            Endereco enderecoBuscado = BuscarPorId(id);

            if(enderecoBuscado != null)
            {
                try
                {
                    ctx.Endereco.Remove(enderecoBuscado);
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
    }
}
