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

        public List<TipoUsuario> Listar() => ctx.TipoUsuario.ToList();
        public TipoUsuario BuscarPorNome(string titulo) => ctx.TipoUsuario.FirstOrDefault(t => t.TituloTipoUsuario == titulo);

        public TipoUsuario BuscarPorId(int id) => ctx.TipoUsuario.FirstOrDefault(t => t.IdTipoUsuario == id);

        public string Cadastrar(TipoUsuario data)
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

        public string Atualizar(TipoUsuario tituloNovo, int id)
        {
            TipoUsuario tipoUsuariobuscado = BuscarPorId(id);

            if(tipoUsuariobuscado != null && Convert.ToBoolean(id))
            {
                tipoUsuariobuscado.TituloTipoUsuario = tituloNovo.TituloTipoUsuario;

                ctx.TipoUsuario.Update(tipoUsuariobuscado);

                ctx.SaveChanges();

                return $"tipo usuario {id} atualizado com sucesso";
            }
            else
            {
                return "tipo usuario não encontrado";
            }

        }

        public string Deletar(int id)
        {
            TipoUsuario tipoUsuarioBuscado = BuscarPorId(id);

            if(tipoUsuarioBuscado != null)
            {
                ctx.TipoUsuario.Remove(tipoUsuarioBuscado);

                ctx.SaveChanges();

                return $"tipo usuario id:{id} excluido com sucesso";
            }
            else
            {
                return $"tipo usuario id:{id} não encontrado";
            }
        }        
    }
}
