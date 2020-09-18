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
    public class FormacaoAcademicaController : ControllerBase
    {
        private IFormacaoAcademica _formacaoAcademicaRepository;

        public FormacaoAcademicaController()
        {
            _formacaoAcademicaRepository = new FormacaoAcademicaRepository();

        }

        /// <summary>
        /// Lista todas Formações Academica
        /// </summary>
        [HttpGet]
        public IActionResult Get() => Ok(_formacaoAcademicaRepository.Listar());

        /// <summary>
        /// Cadastra uma Formação Academica
        /// </summary>
        [HttpPost]
        public IActionResult Post(FormacaoAcademica data)
        {
            TypeMessage returnRepository = _formacaoAcademicaRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza uma Formação Academica
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, FormacaoAcademica data)
        {
            TypeMessage returnRepository = _formacaoAcademicaRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta uma Formação Academica
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _formacaoAcademicaRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

    }

}
