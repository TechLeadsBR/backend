using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class InscricaoEmpregoController : ControllerBase
    {
        private IInscricaoEmprego _inscricaoEmpregoRepository;
        private Functions _functions;

        public InscricaoEmpregoController()
        {
            _inscricaoEmpregoRepository = new InscricaoEmpregoRepository();
            _functions = new Functions();
        }

        /// <summary>
        /// Lista todas Inscrições de Empregos
        /// </summary>
        [Authorize(Roles = "1,2,3")]
        [HttpGet]
        public IActionResult Get()
        {
            string token = HttpContext.Request.Headers["Authorization"][0].Split(" ")[1];
            string jti = _functions.GetClaimInBearerToken(token, "jti");
            return Ok(_inscricaoEmpregoRepository.Listar(Convert.ToInt32(jti)));
        }

        /// <summary>
        /// Cadastra uma Inscricao de Emprego
        /// </summary>
        [HttpPost]
        public IActionResult Post(InscricaoEmprego data)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza uma Inscricao de Emprego
        /// </summary>
        [Authorize(Roles = "3")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, InscricaoEmprego data)
        {
            TypeMessage returnRepository = _inscricaoEmpregoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta uma Inscricao de Emprego
        /// </summary>
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
