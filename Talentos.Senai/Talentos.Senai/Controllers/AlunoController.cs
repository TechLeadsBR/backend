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
    public class AlunoController : ControllerBase
    {
        private IAluno _alunoRepository;
        private Functions _functions;

        public AlunoController()
        {
            _alunoRepository = new AlunoRepository();
            _functions = new Functions();
        }

        /// <summary>
        /// Listar todos Alunos
        /// </summary>
        [Authorize(Roles = Users.Administrator)]
        [HttpGet]
        public IActionResult Get() => Ok(_alunoRepository.Listar());

        [Authorize(Roles = Users.Student)]
        [HttpGet("id")]
        public IActionResult GetById()
        {
            var token = Request.Headers["Authorization"][0].Split(' ')[1];
            string jti = _functions.GetClaimInBearerToken(token, "jti");
            return Ok(_alunoRepository.BuscarPorId(Convert.ToInt32(jti), false));
        }

        /// <summary>
        /// Cadastra um Aluno
        /// </summary>
        [HttpPost]
        public IActionResult Post(Aluno data)
        {
            TypeMessage returnRepository = _alunoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza um Aluno
        /// </summary>
        [Authorize(Roles = Users.Administrator + ", " + Users.Student)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno data)
        {
            TypeMessage returnRepository = _alunoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta um Aluno
        /// </summary>
        [Authorize(Roles = Users.Administrator + ", " + Users.Student)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _alunoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}