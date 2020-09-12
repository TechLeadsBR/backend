using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private IAdministrador _administradorRepository;

        public AdministradorController()
        {
            _administradorRepository = new AdministradorRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_administradorRepository.Listar());

    }
}