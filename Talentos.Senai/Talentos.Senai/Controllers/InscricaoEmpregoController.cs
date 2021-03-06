﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class InscricaoEmpregoController : ControllerBase
    {
        private IInscricaoEmprego _inscricaoEmpregoRepository;
        private Functions _functions;

        public InscricaoEmpregoController()
        {
            _inscricaoEmpregoRepository = new InscricaoEmpregoRepository();
            _functions = new Functions();
        }

        /// <summary>
        /// Lista todas Inscrições de Empregos
        /// </summary>
        [Authorize(Roles = Users.All)]
        [HttpGet]
        public IActionResult Get()
        {
            string token = HttpContext.Request.Headers["Authorization"][0].Split(" ")[1];
            string jti = _functions.GetClaimInBearerToken(token, "jti");
            string role = _functions.GetClaimInBearerToken(token, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            return Ok(_inscricaoEmpregoRepository.Listar(Convert.ToInt32(jti), role));
        }

        /// <summary>
        /// Cadastra uma Inscricao de Emprego
        /// </summary>
        [Authorize(Roles = Users.Student)]
        [HttpPost]
        public IActionResult Post(InscricaoEmprego data)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza uma Inscricao de Emprego
        /// </summary>
        [Authorize(Roles = Users.Company)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, InscricaoEmprego data)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta uma Inscricao de Emprego
        /// </summary>
        [Authorize(Roles = Users.Administrator + "," + Users.Company)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
