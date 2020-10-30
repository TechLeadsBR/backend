using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IVagaEmprego
    {
        List<VagaEmprego> Listar();

        VagaEmprego BuscarPorId(int id);

        List<VagaEmprego> FiltrarPorEmpresa(int id);

        List<VagaEmprego> FiltroGeral(string palavra);

        TypeMessage Cadastrar(VagaEmprego data);

        TypeMessage Atualizar(int id, VagaEmprego data);

        TypeMessage Deletar(int id);
    }
}
