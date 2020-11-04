using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;

namespace Talentos.Senai.Repositories
{
    public class MetricsRepository : IMetrics
    {
        public object returnMetrics()
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                int studentsCount = ctx.Aluno.Count();
                int registrationsCount = ctx.InscricaoEmprego.Count();
                int jobsCount = ctx.VagaEmprego.Count();
                int companysCount = ctx.Empresa.Count();

                return new
                {
                    aluno = studentsCount,
                    empresa = companysCount,
                    vagas = jobsCount,
                    inscricoes = registrationsCount
                };
            }
        }
    }
}
