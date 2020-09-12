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
    public class AlunoController : ControllerBase
    {
        private IAluno _alunoRepository;

        public AlunoController()
        {
            _alunoRepository = new AlunoRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_alunoRepository.Listar());

        [HttpPost]
        public IActionResult Post(Aluno data)
        {
            TypeMessage returnRepository = _alunoRepository.Cadastrar(data);
            if (returnRepository.ok)return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno data)
        {
            TypeMessage returnRepository = _alunoRepository.Atualizar(id, data);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _alunoRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}