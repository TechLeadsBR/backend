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
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ExperienciaProfissionalController : ControllerBase
    {
        private IExperienciaProfissional _experienciaProfissionalRepository;
        private Functions _functions;

        public ExperienciaProfissionalController()
        {
            _experienciaProfissionalRepository = new ExperienciaProfissionalRepository();
            _functions = new Functions();
        }

        /// <summary>
        /// Lista todas Experiencias Profissional
        /// </summary>
        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public IActionResult Get()
        {
            string token = HttpContext.Request.Headers["Authorization"][0].Split(" ")[1];
            string jti = _functions.GetClaimInBearerToken(token, "jti");
            return Ok(_experienciaProfissionalRepository.Listar(Convert.ToInt32(jti)));
        }

        /// <summary>
        /// Cadastra uma Experiencia Profissional
        /// </summary>
        [Authorize(Roles = "1, 2")]
        [HttpPost]
        public IActionResult Post(ExperienciaProfissional data)
        {
            TypeMessage returnRepository = _experienciaProfissionalRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza uma Experiencia Profissional
        /// </summary>
        [Authorize(Roles = "1, 2")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, ExperienciaProfissional data)
        {
            TypeMessage returnRepository = _experienciaProfissionalRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta uma Experiencia Profissional
        /// </summary>
        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _experienciaProfissionalRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
