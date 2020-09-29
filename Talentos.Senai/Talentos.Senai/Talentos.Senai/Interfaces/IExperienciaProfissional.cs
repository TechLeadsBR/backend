using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IExperienciaProfissional
    {
        List<ExperienciaProfissional> Listar(int jti);

        ExperienciaProfissional BuscarPorId(int id);

        TypeMessage Cadastrar(ExperienciaProfissional data);

        TypeMessage Atualizar(int id, ExperienciaProfissional data);

        TypeMessage Deletar(int id);
    }
}
