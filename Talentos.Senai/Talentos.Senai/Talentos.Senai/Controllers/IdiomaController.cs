using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdiomaController : ControllerBase
    {
        private IIdioma _idiomaRepository;

        public IdiomaController()
        {
            _idiomaRepository = new IdiomaRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_idiomaRepository.Listar());

        [HttpPost]
        public IActionResult Post(Idioma data)
        {
            TypeMessage returnRepository = _idiomaRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Idioma data)
        {
            TypeMessage returnRepository = _idiomaRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _idiomaRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}