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
    public class InscricaoEmpregoController : ControllerBase
    {
        private IInscricaoEmprego _inscricaoEmpregoRepository;

        public InscricaoEmpregoController()
        {
            _inscricaoEmpregoRepository = new InscricaoEmpregoRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_inscricaoEmpregoRepository.Listar());

        [HttpPost]
        public IActionResult Post(InscricaoEmprego data)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "3")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, InscricaoEmprego data)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
