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
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuario _tipoUsuarioRepository;

        public TipoUsuarioController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get() => Ok(_tipoUsuarioRepository.Listar());

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(TipoUsuario data)
        {
            TypeMessage returnRepository = _tipoUsuarioRepository.Cadastrar(data);
            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(TipoUsuario data, int id)
        {
            TypeMessage returnRepository = _tipoUsuarioRepository.Atualizar(data, id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage returnRepository = _tipoUsuarioRepository.Deletar(id);
            if (returnRepository.ok) return Ok(returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}
