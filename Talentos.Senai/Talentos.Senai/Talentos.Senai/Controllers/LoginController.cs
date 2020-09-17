using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.General;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogin _loginRepository;

        public LoginController()
        {
            _loginRepository = new LoginRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel data)
        {
            Usuario usuarioBuscado = _loginRepository.BuscarUsuario(data);
            if (usuarioBuscado.aluno == null && usuarioBuscado.empresa == null) return BadRequest(new { message = "E-mail ou senha invalido" });


            return Ok(
                
                     _loginRepository.CreateToken((usuarioBuscado.aluno.Email ?? usuarioBuscado.empresa.Email),
                (usuarioBuscado.aluno.IdTipoUsuario.ToString() ?? usuarioBuscado.empresa.IdTipoUsuario.ToString()))
            );
        }
    }
}