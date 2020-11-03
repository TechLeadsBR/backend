using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IInscricaoEmprego
    {
        List<InscricaoEmprego> Listar(int jti, string roleUser);

        InscricaoEmprego BuscarporIdAlunoeVagaEmprego(int idAluno, int idVagaEmprego);

        InscricaoEmprego BuscarporIdInscricao(int id);

        TypeMessage Cadastrar(InscricaoEmprego data);

        TypeMessage Atualizar(int id, InscricaoEmprego data);

        TypeMessage Deletar(int id);
    }
}
