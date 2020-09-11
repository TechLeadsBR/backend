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

            if (data.TituloTipoUsuario != null)
            {
                TipoUsuario findTipoUsuario = _tipoUsuarioRepository.BuscarPorNome(data.TituloTipoUsuario);

                if (findTipoUsuario == null)
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
                    message = "parametro esperado: titulotipousuario"
                };

                return BadRequest(response);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(TipoUsuario tituloNovo, int id)
        {
            if (tituloNovo.TituloTipoUsuario != null)
            {
                var response = new
                {
                    messsage = _tipoUsuarioRepository.Atualizar(tituloNovo, id)
                };

                return Ok(response);

            }
            else {
                var response = new
                {
                    message = "parametro esperado: titulotipousuario"
                };

                return BadRequest(response);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (Convert.ToBoolean(id))
            {
                var response = new
                {
                    message = _tipoUsuarioRepository.Deletar(id)
                };

                return Ok(response);
            }
            else
            {
                var response = new
                {
                    message = "parametro esperado: id"
                };

                return BadRequest(response);
            }
        }
    }
}
