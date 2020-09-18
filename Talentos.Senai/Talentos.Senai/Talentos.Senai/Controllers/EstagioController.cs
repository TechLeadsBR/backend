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
    public class EstagioController : ControllerBase
    {
        private IEstagio _estagioRepository;

        public EstagioController()
        {
            _estagioRepository = new EstagioRepository();
        }

        /// <summary>
        /// Lista todos os Estagios
        /// </summary>
        /// <returns>Retorna uma lista de estudios</returns>
        [HttpGet]
        public IActionResult Get() => Ok(_estagioRepository.Listar());

        /// <summary>
        /// Cadastra um Estagio
        /// </summary>
        /// <param name="data">Objeto novoEstudio que será cadastrado</param>
        [HttpPost]
        public IActionResult Post(Estagio data)
        {
            TypeMessage returnRepository = _estagioRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza um Estagio
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Estagio data)
        {
            TypeMessage returnRepository = _estagioRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta um Estagio
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _estagioRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}