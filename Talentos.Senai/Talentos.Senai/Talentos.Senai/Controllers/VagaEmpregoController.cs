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
    public class VagaEmpregoController : ControllerBase
    {
        private IVagaEmprego _vagaEmpregoRepository;

        public VagaEmpregoController()
        {
            _vagaEmpregoRepository = new VagaEmpregoRepository();
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public IActionResult Get() => Ok(_vagaEmpregoRepository.Listar());

        [Authorize(Roles = "1, 3")]
        [HttpPost]
        public IActionResult Post(VagaEmprego data)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1, 3")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, VagaEmprego data)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1, 3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
