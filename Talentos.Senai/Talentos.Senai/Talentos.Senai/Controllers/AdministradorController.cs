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
    public class AdministradorController : ControllerBase
    {
        private IAdministrador _administradorRepository;

        public AdministradorController()
        {
            _administradorRepository = new AdministradorRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get() => Ok(_administradorRepository.Listar());

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Administrador data)
        {
            TypeMessage returnRepository = _administradorRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Administrador data)
        {
            TypeMessage returnRepository = _administradorRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _administradorRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

    }
}