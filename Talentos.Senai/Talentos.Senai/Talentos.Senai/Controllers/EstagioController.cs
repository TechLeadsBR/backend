using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class EstagioController : ControllerBase
    {
        private IEstagio _estagioRepository;

        public EstagioController()
        {
            _estagioRepository = new EstagioRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get() => Ok(_estagioRepository.Listar());

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Estagio data)
        {
            TypeMessage returnRepository = _estagioRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Estagio data)
        {
            TypeMessage returnRepository = _estagioRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _estagioRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}