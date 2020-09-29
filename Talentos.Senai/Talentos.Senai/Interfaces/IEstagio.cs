using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IEstagio
    {
        List<Estagio> Listar();

        Estagio BuscarPorIdAluno(int id);

        Estagio BuscarPorId(int id);

        TypeMessage Cadastrar(Estagio data);

        TypeMessage Atualizar(int id, Estagio data);

        TypeMessage Deletar(int id);
    }
}
