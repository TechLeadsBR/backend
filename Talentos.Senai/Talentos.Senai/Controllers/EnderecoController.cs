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
    public class EnderecoController : ControllerBase
    {
        private IEndereco _enderecoRepository;

        public EnderecoController()
        {
            _enderecoRepository = new EnderecoRepository();
        }

        /// <summary>
        /// Lista todos Endereços
        /// </summary>
        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpGet]
        public IActionResult Get() => Ok(_enderecoRepository.Listar());

        /// <summary>
        /// Lista Endereços Por Id
        /// </summary>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_enderecoRepository.BuscarPorId(id));

        /// <summary>
        /// Cadastra um Endereço
        /// </summary>
        [HttpPost]
        public IActionResult Post(Endereco novoEndereco)
        {
            TypeMessage returnRepository = _enderecoRepository.CadastrarEndereco(novoEndereco);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Atualiza um Endereço
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Endereco enderecoAtualizado)
        {
            TypeMessage returnRepository = _enderecoRepository.AtualizarEndereco(id, enderecoAtualizado);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        /// <summary>
        /// Deleta um Endereço
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _enderecoRepository.DeletarEndereco(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }    
    }
}
