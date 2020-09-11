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
        private General _functions;

        public EmpresaController()
        {
            _empresaRepository = new EmpresaRepository();
            _functions = new General();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_empresaRepository.Listar());
        }

        [HttpPost]
        public IActionResult Post(Empresa novoEmpresa)
        {
            // Faz a chamada para o método
            bool returnFunction = _empresaRepository.Cadastrar(novoEmpresa);

            if (returnFunction) {

                return Ok(_functions.returnResponse(new
                {
                    message = "Empresa cadastrada com sucesso"
                }));
            }
            else
            {
                return BadRequest(_functions.returnResponse(new
                {
                    message = "Ocorreu algum erro, cnpj ou email já foram cadastrados"
                }));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleteEmpresa = _empresaRepository.Deletar(id);

            if (deleteEmpresa)
            {
                return Ok(_functions.returnResponse(new
                {
                    message = $"empresa Id:{id} deletado"
                }));
            }
            else
            {
                 return BadRequest(new
                {
                    message = $"empresa Id:{id} não foi deletado"
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Empresa empresaAtualizado)
        {

            bool resultUpdate = _empresaRepository.Atualizar(id, empresaAtualizado);

            if (resultUpdate)
            {                
                return Ok(new
                {
                    message = $"empresa id:{id} atualizada"
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = $"empresa id:{id} não atualizada"
                });
            }
        }
    }
}
