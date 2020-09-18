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
    [Route("[controller]")]
    [ApiController]
    public class VagaEmpregoController : ControllerBase
    {
        private IVagaEmprego _vagaEmpregoRepository;

        public VagaEmpregoController()
        {
            _vagaEmpregoRepository = new VagaEmpregoRepository();
        }

        /// <summary>
        /// Lista Todas Vagas de Emprego
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok(_vagaEmpregoRepository.Listar());

        /// <summary>
        /// Cadastra uma Vaga de Emprego
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(VagaEmprego data)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza uma Vaga de Emprego
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, VagaEmprego data)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta uma Vaga de Emprego
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _vagaEmpregoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
