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
    public class VagaEmpregoRepository : IVagaEmprego
    {
        private TalentosContext ctx = new TalentosContext();
        private readonly Functions _functions = new Functions();
        private readonly IEmpresa _empresaRepository = new EmpresaRepository();
        private readonly string table = "vagaemprego";

        public List<VagaEmprego> Listar() => ctx.VagaEmprego.Include(v => v.IdEmpresaNavigation).ToList();

        public VagaEmprego BuscarPorId(int id) => ctx.VagaEmprego.FirstOrDefault(v => v.IdVagaEmprego == id);

        public TypeMessage Cadastrar(VagaEmprego data)
        {
            if (data != null)
            {
                Empresa empresaBuscada = _empresaRepository.BuscarPorId(data.IdEmpresa.GetValueOrDefault());

                if(empresaBuscada != null)
                {
                    try
                    {
                        ctx.VagaEmprego.Add(data);
                        ctx.SaveChanges();

                        string okMessage = _functions.defaultMessage(table, "ok");
                        return _functions.replyObject(okMessage, true);

                    } catch(Exception error)
                    {
                        Console.WriteLine(error);
                        string errorMessage = _functions.defaultMessage(table, "error");
                        return _functions.replyObject(errorMessage, false);
                    }
                }else
                {
                    string notFoundMessage = _functions.defaultMessage("empresa", "notfound");
                    return _functions.replyObject(notFoundMessage, false);
                }


            } else
            {
                string dataMessage = _functions.defaultMessage(table, "data");
                return _functions.replyObject(dataMessage, false);
            }
        }

        public TypeMessage Atualizar(int id, VagaEmprego data)
        {
            VagaEmprego vagaBuscada = BuscarPorId(id);

            if(vagaBuscada != null)
            {
                Empresa empresaBuscada = _empresaRepository.BuscarPorId(data.IdEmpresa.GetValueOrDefault());

                if(empresaBuscada != null)
                {
                    try
                    {
                        vagaBuscada.Titulo = data.Titulo ?? vagaBuscada.Titulo;
                        vagaBuscada.Nivel = data.Nivel ?? vagaBuscada.Nivel;
                        vagaBuscada.Cidade = data.Cidade ?? vagaBuscada.Cidade;
                        vagaBuscada.DescricaoVaga = data.DescricaoVaga ?? vagaBuscada.DescricaoVaga;
                        vagaBuscada.Habilidade = data.Habilidade ?? vagaBuscada.Habilidade;
                        vagaBuscada.RemuneracaoBeneficio = data.RemuneracaoBeneficio ?? vagaBuscada.RemuneracaoBeneficio;
                        vagaBuscada.TipoContrato = data.TipoContrato ?? vagaBuscada.TipoContrato;
                        vagaBuscada.IdEmpresa = data.IdEmpresa ?? vagaBuscada.IdEmpresa;

                        ctx.VagaEmprego.Update(vagaBuscada);
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
                    string notFoundMessage = _functions.defaultMessage("empresa", "notfound");
                    return _functions.replyObject(notFoundMessage, false);
                }

            }else
            {
                string notFoundMessage = _functions.defaultMessage(table, "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public TypeMessage Deletar(int id)
        {
            VagaEmprego vagaBuscada = BuscarPorId(id);

            if(vagaBuscada != null)
            {
                try
                {
                    ctx.VagaEmprego.Remove(vagaBuscada);
                    ctx.SaveChanges();

                    string okMessage = _functions.defaultMessage(table, "ok");
                    return _functions.replyObject(okMessage, true);

                } catch( Exception error)
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
    }
}