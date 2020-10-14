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

        /// <summary>
        /// Lista Todas Vagas de Emprego
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public IActionResult Get() => Ok(_vagaEmpregoRepository.Listar());

        /// <summary>
        /// Lista vagas de emprego de acordo com o filtro
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        //[Authorize(Roles = "1, 2, 3")]
        [HttpGet("{palavra}")]
        public IActionResult Get(string palavra) => Ok(_vagaEmpregoRepository.FiltroGeral(palavra));

        /// <summary>
        /// Cadastra uma Vaga de Emprego
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize(Roles = "1, 3")]
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
        [Authorize(Roles = "1, 3")]
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
