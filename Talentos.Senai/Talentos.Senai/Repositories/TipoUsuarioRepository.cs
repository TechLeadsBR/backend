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
        public readonly Functions _functions;
        private readonly string table;
        public TipoUsuarioRepository()
        {
            _functions = new Functions();
            table = "tipousuario";
        }

        public List<TipoUsuario> Listar()
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.TipoUsuario.ToList();
            }
        }
        public TipoUsuario BuscarPorNome(string titulo)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.TipoUsuario.FirstOrDefault(t => t.TituloTipoUsuario == titulo);
            }
        }

        public TipoUsuario BuscarPorId(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.TipoUsuario.FirstOrDefault(t => t.IdTipoUsuario == id);
            }
        }

        public TypeMessage Cadastrar(TipoUsuario data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                TipoUsuario tipoUsuarioBuscado = BuscarPorNome(data.TituloTipoUsuario);

                if (tipoUsuarioBuscado == null)
                {
                    try
                    {
                        ctx.TipoUsuario.Add(data);
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
                    string existsMesssage = _functions.defaultMessage(table, "exists");

                    return _functions.replyObject(existsMesssage, false);
                }
            }
        }

        public TypeMessage Atualizar(TipoUsuario tituloNovo, int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                TipoUsuario tipoUsuarioBuscado = BuscarPorId(id);

                if (tipoUsuarioBuscado != null)
                {
                    try
                    {
                        tipoUsuarioBuscado.TituloTipoUsuario = tituloNovo.TituloTipoUsuario;
                        ctx.TipoUsuario.Update(tipoUsuarioBuscado);
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
        }

        public TypeMessage Deletar(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                TipoUsuario tipoUsuarioBuscado = BuscarPorId(id);

                if (tipoUsuarioBuscado != null)
                {
                    try
                    {
                        ctx.TipoUsuario.Remove(tipoUsuarioBuscado);
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
        }
    }
}
