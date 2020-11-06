using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class AdministradorRepository : IAdministrador
    {
        private readonly Functions _functions;
        private readonly string table;

        public AdministradorRepository()
        {
            _functions = new Functions();
            table = "administrador";
        }

        public List<Administrador> Listar()
        {
            using(TalentosContext ctx = new TalentosContext())
            {
                return ctx.Administrador
                    .Select(a => new Administrador { 
                        Cpf = a.Cpf,
                        Email = a.Email,
                        Nome = a.Nome,
                        IdAdministrador = a.IdAdministrador
                    })
                    .ToList();
            }
        }

        public Administrador BuscarPorId(int id)
        {
            using(TalentosContext ctx = new TalentosContext())
            {
                return ctx.Administrador.FirstOrDefault(a => a.IdAdministrador == id);
            }
        }

        public Administrador BuscarPorEmaileCpf(string email, string cpf)
        {
            using(TalentosContext ctx = new TalentosContext())
            {
                return ctx.Administrador.FirstOrDefault(a => a.Email == email || a.Cpf == cpf);
            }
        }

        public TypeMessage Cadastrar(Administrador data)
        {
            using(TalentosContext ctx = new TalentosContext())
            {
                Administrador administradorBuscado = BuscarPorEmaileCpf(data.Email, data.Cpf);

                if (administradorBuscado == null)
                {
                    try
                    {
                        ctx.Administrador.Add(data);
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
                    string existsMessage = _functions.defaultMessage(table, "exists");
                    return _functions.replyObject(existsMessage, false);
                }
            }
        }

        public TypeMessage Atualizar(int id, Administrador data)
        {
            using(TalentosContext ctx = new TalentosContext())
            {
                Administrador administradorBuscado = BuscarPorId(id);

                if (administradorBuscado != null)
                {
                    try
                    {
                        administradorBuscado.Nome = data.Nome ?? administradorBuscado.Nome;
                        administradorBuscado.Email = data.Email ?? administradorBuscado.Email;
                        administradorBuscado.Senha = data.Senha ?? administradorBuscado.Senha;
                        administradorBuscado.Cpf = data.Cpf ?? administradorBuscado.Cpf;
                        administradorBuscado.IdTipoUsuario = data.IdTipoUsuario ?? administradorBuscado.IdTipoUsuario;

                        ctx.Administrador.Update(administradorBuscado);
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
            using(TalentosContext ctx = new TalentosContext())
            {
                Administrador administradorBuscado = BuscarPorId(id);

                if (administradorBuscado != null)
                {
                    try
                    {
                        ctx.Administrador.Remove(administradorBuscado);
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
