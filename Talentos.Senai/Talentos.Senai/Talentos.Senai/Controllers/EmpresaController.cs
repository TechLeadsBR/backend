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
    public class EmpresaController : ControllerBase
    {
        private IEmpresa _empresaRepository;


        public EmpresaController()
        {
            _empresaRepository = new EmpresaRepository();
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public IActionResult Get() => Ok(_empresaRepository.Listar());

        [HttpPost]
        public IActionResult Post(Empresa novoEmpresa)
        {
            TypeMessage returnRepository = _empresaRepository.Cadastrar(novoEmpresa);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1, 3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _empresaRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Empresa empresaAtualizado)
        {
            TypeMessage returnRepository = _empresaRepository.Atualizar(id, empresaAtualizado);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
