using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;

namespace Talentos.Senai.Interfaces
{
    interface IIdioma
    {
        List<Idioma> Listar();

        TypeMessage Cadastrar(Idioma data);

        TypeMessage Atualizar(int id, Idioma data);

        TypeMessage Deletar(int id);
    }
}
