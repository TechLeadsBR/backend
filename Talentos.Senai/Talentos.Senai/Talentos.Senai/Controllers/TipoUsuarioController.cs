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
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuario _tipoUsuarioRepository;
    
        public TipoUsuarioController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }

        [HttpPost]
        public IActionResult Post(TipoUsuario data)
        {
            TipoUsuario findTipoUsuario = _tipoUsuarioRepository.BuscarPorNome(data.TituloTipoUsuario);

            if(data.TituloTipoUsuario != null)
            {
                if(findTipoUsuario == null)
                {
                    var response = new
                    {
                        message = _tipoUsuarioRepository.Cadastrar(data)
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        message = $"titulotipousuario ja existente"
                    };

                    return BadRequest(response);
                }

            }
            else
            {
                var response = new
                {
                    message = $"parametro esperado: titulotipousuario"
                };

                return BadRequest(response);
            }
                
        }
    }
}
