using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Domains;
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
    }
}