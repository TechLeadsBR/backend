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
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class IdiomaController : ControllerBase
    {
        private IIdioma _idiomaRepository;

        public IdiomaController()
        {
            _idiomaRepository = new IdiomaRepository();
        }

        /// <summary>
        /// Lista todos os Idiomas
        /// </summary>
        [Authorize(Roles = Users.Administrator + "," + Users.Student)]
        [HttpGet]
        public IActionResult Get() => Ok(_idiomaRepository.Listar());

        /// <summary>
        /// Cadastra um Idioma
        /// </summary>
        [Authorize(Roles = Users.Administrator + "," + Users.Student)]
        [HttpPost]
        public IActionResult Post(Idioma data)
        {
            TypeMessage returnRepository = _idiomaRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza um Idioma
        /// </summary>
        [Authorize(Roles = Users.Administrator + "," + Users.Student)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Idioma data)
        {
            TypeMessage returnRepository = _idiomaRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta um Idioma
        /// </summary>
        [Authorize(Roles = Users.Administrator + "," + Users.Student)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _idiomaRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}