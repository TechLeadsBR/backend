using System.Collections.Generic;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Domains;
using System.Linq;
using System;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        TalentosContext ctx = new TalentosContext();

        public General _functions;
        private string table;

        public TipoUsuarioRepository()
        {
            _functions = new General();
            table = "tipousuario";
        }

        public List<TipoUsuario> Listar() => ctx.TipoUsuario.ToList();
        public TipoUsuario BuscarPorNome(string titulo) => ctx.TipoUsuario.FirstOrDefault(t => t.TituloTipoUsuario == titulo);

        public TipoUsuario BuscarPorId(int id) => ctx.TipoUsuario.FirstOrDefault(t => t.IdTipoUsuario == id);

        public TypeMessage Cadastrar(TipoUsuario data)
        {
            TipoUsuario tipoUsuarioBuscado = BuscarPorNome(data.TituloTipoUsuario);

            if(tipoUsuarioBuscado == null)
            {                    
                ctx.TipoUsuario.Add(data);
                ctx.SaveChanges();

                string okMessage = _functions.defaultMessage(table, "ok");

                return _functions.replyObject(okMessage, true);
            }
            else
            {
                string existsMesssage = _functions.defaultMessage(table, "existente");

                return _functions.replyObject(existsMesssage, false);
            }
        }

        public TypeMessage Atualizar(TipoUsuario tituloNovo, int id)
        {
            TipoUsuario tipoUsuariobuscado = BuscarPorId(id);

            if(tipoUsuariobuscado != null)
            {
                tipoUsuariobuscado.TituloTipoUsuario = tituloNovo.TituloTipoUsuario;
                ctx.TipoUsuario.Update(tipoUsuariobuscado);
                ctx.SaveChanges();

                string okMessage = _functions.defaultMessage(table, "ok");

                return _functions.replyObject(okMessage, true);
            }
            else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }

        }

        public TypeMessage Deletar(int id)
        {
            TipoUsuario tipoUsuarioBuscado = BuscarPorId(id);

            if(tipoUsuarioBuscado != null)
            {
                try
                {
                    ctx.TipoUsuario.Remove(tipoUsuarioBuscado);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");
                    return _functions.replyObject(okMessage, true);
                } catch(Exception error)
                {
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
