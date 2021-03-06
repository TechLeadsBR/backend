﻿using Microsoft.EntityFrameworkCore;
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
        private readonly Functions _functions;
        private ITipoUsuario _tipoUsuarioRepository;
        private readonly string table;

        public EmpresaRepository()
        {
            _functions = new Functions();
            _tipoUsuarioRepository = new TipoUsuarioRepository();
            table = "empresa";
        }

        public Empresa BuscarPorId(int id, bool allData = true)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                if (allData)
                {
                    return ctx.Empresa.FirstOrDefault(e => e.IdEmpresa == id);
                }
                else
                {
                    return ctx.Empresa.Select(e => new Empresa
                    {
                        IdEmpresa = e.IdEmpresa,
                        RazaoSocial = e.RazaoSocial,
                        Email = e.Email,
                        Cnpj = e.Cnpj,
                        AtividadeEconomica = e.AtividadeEconomica,
                        Telefone = e.Telefone,
                        TelefoneDois = e.TelefoneDois,
                        NomeFoto = e.NomeFoto,
                        DescricaoEmpresa = e.DescricaoEmpresa
                    }).FirstOrDefault(e => e.IdEmpresa == id);
                }
            }
        }
        public List<Empresa> Listar()
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                return ctx.Empresa.Select(e => new Empresa
                {
                    IdEmpresa = e.IdEmpresa,
                    RazaoSocial = e.RazaoSocial,
                    Email = e.Email,
                    Cnpj = e.Cnpj,
                    AtividadeEconomica = e.AtividadeEconomica,
                    Telefone = e.Telefone,
                    TelefoneDois = e.TelefoneDois,
                    NomeFoto = e.NomeFoto,
                    DescricaoEmpresa = e.DescricaoEmpresa
                }).ToList();
            }
        }

        public TypeMessage Atualizar(int id, Empresa empresaAtualizado)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                Empresa empresaParaAtualizar = BuscarPorId(id);

                if (empresaParaAtualizar != null)
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
        }


        /// <summary>
        /// Cadastra uma nova Empresa
        /// </summary>
        /// <param name="novoEmpresa">Objeto novoEmpresa que será cadastrada</param>
        public TypeMessage Cadastrar(Empresa novoEmpresa)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                Empresa empresaExiste = ctx.Empresa.FirstOrDefault(e => e.Cnpj == novoEmpresa.Cnpj || e.Email == novoEmpresa.Email);

                if (empresaExiste == null)
                {
                    TipoUsuario tipoUsuarioBuscado = _tipoUsuarioRepository.BuscarPorId(novoEmpresa.IdTipoUsuario.GetValueOrDefault());

                    if (tipoUsuarioBuscado != null)
                    {
                        try
                        {
                            ctx.Empresa.Add(novoEmpresa);
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
                        string notFoundMessage = _functions.defaultMessage("tipousuario", "notfound");
                        return _functions.replyObject(notFoundMessage, false);
                    }

                }
                else
                {
                    string notFoundMessage = _functions.defaultMessage(table, "exists");
                    return _functions.replyObject(notFoundMessage, false);
                }
            }
        }

        /// <summary>
        /// Deletar uma empresa
        /// </summary>
        /// <param name="id">ID da empresa que sera deleada</param>
        public TypeMessage Deletar(int id)
        {
            using (TalentosContext ctx = new TalentosContext())
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
