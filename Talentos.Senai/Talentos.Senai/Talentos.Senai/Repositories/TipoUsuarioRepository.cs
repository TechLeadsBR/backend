using System.Collections.Generic;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Domains;
using System.Linq;
using System;

namespace Talentos.Senai.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        TalentosContext ctx = new TalentosContext();

        public List<TipoUsuario> Listar()
        {
            return ctx.TipoUsuario.ToList();
        }

        public TipoUsuario BuscarPorNome(string nome)
        {
            return ctx.TipoUsuario.FirstOrDefault(t => t.TituloTipoUsuario == nome);
        }

        public string Cadastrar(TipoUsuario data)
        {
            try
            {
                if(data.TituloTipoUsuario != null)
                {                    
                    ctx.TipoUsuario.Add(data);

                    ctx.SaveChanges();

                    return $"tipo usuario {data.TituloTipoUsuario} cadastrado com sucesso";
                }
                else
                {
                    return $"parametro esperado: titulotipousuario";
                }

            }
            catch (ArgumentException _)
            {
                return $"falha ao cadastrar tipo usuario {data.TituloTipoUsuario}";
            }
        }

        public string Atualizar(TipoUsuario data, int id)
        {
            throw new System.NotImplementedException();
        }

        public string Deletar(int id)
        {
            throw new System.NotImplementedException();
        }        
    }
}
