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
    public class IdiomaController : ControllerBase
    {
        private IIdioma _idiomaRepository;

        public IdiomaController()
        {
            _idiomaRepository = new IdiomaRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_idiomaRepository.Listar());

        [HttpPost]
        public IActionResult Post(Idioma data)
        {
            TypeMessage returnRepository = _idiomaRepository.Cadastrar(data);

            if (returnRepository.ok) return StatusCode(201, returnRepository);
            else return BadRequest(returnRepository);
        }
    }
}