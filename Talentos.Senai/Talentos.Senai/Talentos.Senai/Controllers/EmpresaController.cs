using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private IEmpresa _empresaRepository;


        public EmpresaController()
        {
            _empresaRepository = new EmpresaRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_empresaRepository.Listar());
        }

        [HttpPost]
        public IActionResult Post(Empresa novoEmpresa)
        {
            TypeMessage returnFunction = _empresaRepository.Cadastrar(novoEmpresa);

            if (returnFunction.ok) {

                return Ok(returnFunction);
            }
            else
            {
                return BadRequest(returnFunction);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TypeMessage deleteEmpresa = _empresaRepository.Deletar(id);

            if (deleteEmpresa.ok)
            {
                return Ok(deleteEmpresa);
            }
            else
            {
                 return BadRequest(deleteEmpresa);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Empresa empresaAtualizado)
        {

            TypeMessage resultUpdate = _empresaRepository.Atualizar(id, empresaAtualizado);

            if (resultUpdate.ok)
            {                
                return Ok(resultUpdate);
            }
            else
            {
                return BadRequest(resultUpdate);
            }
        }
    }
}
