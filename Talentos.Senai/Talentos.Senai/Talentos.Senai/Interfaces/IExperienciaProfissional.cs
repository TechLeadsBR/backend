using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IExperienciaAcademica
    {
        List<ExperienciaProfissional> Listar();

        ExperienciaProfissional BuscarPorId(int id);

        TypeMessage Cadastrar(ExperienciaProfissional data);

        TypeMessage Atualizar(int id, ExperienciaProfissional data);

        TypeMessage Deletar(int id);
    }
}
