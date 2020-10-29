using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("aluno")]
        public IActionResult LoginAluno(LoginViewModel data)
        {
            TypeMessage loginAluno = _loginRepository.BuscarAluno(data);
            if (loginAluno.ok) return Ok(loginAluno);
            else return BadRequest(loginAluno);
        }

        [HttpPost("empresa")]
        public IActionResult LoginEmpresa(LoginViewModel data)
        {
            TypeMessage loginEmpresa = _loginRepository.BuscarEmpresa(data);
            if (loginEmpresa.ok) return Ok(loginEmpresa);
            else return BadRequest(loginEmpresa);
        }

        [HttpPost("administrador")]
        public IActionResult LoginAdministrador(LoginViewModel data)
        {
            TypeMessage loginAdministrador = _loginRepository.BuscarAdministrador(data);
            if (loginAdministrador.ok) return Ok(loginAdministrador);
            else return BadRequest(loginAdministrador);
        }
    }
}