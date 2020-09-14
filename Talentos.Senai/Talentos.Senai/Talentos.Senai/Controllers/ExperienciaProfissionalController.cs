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
    public class ExperienciaProfissionalController : ControllerBase
    {
        private IExperienciaProfissional _experienciaProfissionalRepository;

        public ExperienciaProfissionalController()
        {
            _experienciaProfissionalRepository = new ExperienciaProfissionalRepository();

        }

        [HttpGet]
        public IActionResult Get() => Ok(_experienciaProfissionalRepository.Listar());

        [HttpPost]
        public IActionResult Post(ExperienciaProfissional data)
        {
            TypeMessage returnRepository = _experienciaProfissionalRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ExperienciaProfissional data)
        {
            TypeMessage returnRepository = _experienciaProfissionalRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _experienciaProfissionalRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
