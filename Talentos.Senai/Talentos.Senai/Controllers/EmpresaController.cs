﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private IEmpresa _empresaRepository;
        private Functions _functions;


        public EmpresaController()
        {
            _empresaRepository = new EmpresaRepository();
            _functions = new Functions();
        }

        /// <summary>
        /// Lista todas Empresas
        /// </summary>
        /// <returns>Retorna uma lista de Empresas</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get() => Ok(_empresaRepository.Listar());

        [Authorize(Roles = "3")]
        [HttpGet("id")]
        public IActionResult GetById()
        {
            var token = Request.Headers["Authorization"][0].Split(' ')[1];
            int jti = _functions.GetJtiInBearerToken(token);
            return Ok(_empresaRepository.BuscarPorId(jti, false));
        }

        /// <summary>
        /// Cadastra uma Empresa
        /// </summary>
        /// <param name="novoEmpresa"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Empresa novoEmpresa)
        {
            TypeMessage returnRepository = _empresaRepository.Cadastrar(novoEmpresa);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta uma Empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "1, 3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _empresaRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza uma Empresa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empresaAtualizado"></param>
        /// <returns></returns>
        [Authorize(Roles = "1, 3")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Empresa empresaAtualizado)
        {
            TypeMessage returnRepository = _empresaRepository.Atualizar(id, empresaAtualizado);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
