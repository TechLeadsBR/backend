using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IFormacaoAcademica
    { 
        List<FormacaoAcademica> Listar();

        FormacaoAcademica BuscarPorId(int id);

        TypeMessage Cadastrar(FormacaoAcademica data);

        TypeMessage Atualizar(int id, FormacaoAcademica data);

        TypeMessage Deletar(int id);
    }
}
