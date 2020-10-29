using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IAluno
    {
        List<Aluno> Listar();

        Aluno BuscarPorId(int id, bool allData=true);

        TypeMessage Cadastrar(Aluno data);

        TypeMessage Atualizar(int id, Aluno data);

        TypeMessage Deletar(int id);
    }
}
