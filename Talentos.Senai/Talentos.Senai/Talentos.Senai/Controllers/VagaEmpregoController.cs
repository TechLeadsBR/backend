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
    public class VagaEmpregoController : ControllerBase
    {
        private IVagaEmprego _vagaEmpregoRepository;

        public VagaEmpregoController()
        {
            _vagaEmpregoRepository = new VagaEmpregoRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_vagaEmpregoRepository.Listar());

        [HttpPost]
        public IActionResult Post(VagaEmprego data)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, VagaEmprego data)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
