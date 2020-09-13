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
    public class EnderecoController : ControllerBase
    {
        private IEndereco _enderecoRepository;

        public EnderecoController()
        {
            _enderecoRepository = new EnderecoRepository();
        }

        [HttpGet]
        
        public IActionResult Get()
        {
            return Ok(_enderecoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_enderecoRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Post(Endereco novoEndereco)
        {
            _enderecoRepository.CadastrarEndereco(novoEndereco);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Endereco enderecoAtualizado)
        {

            _enderecoRepository.AtualizarEndereco(id, enderecoAtualizado);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _enderecoRepository.DeletarEndereco(id);

            return StatusCode(204);
        }

    
    }
}
