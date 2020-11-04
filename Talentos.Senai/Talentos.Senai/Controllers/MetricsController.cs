using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private IMetrics _metrics;

        public MetricsController()
        {
            _metrics = new MetricsRepository();
        }

        [Authorize(Roles = Users.Administrator)]
        [HttpGet]
        public IActionResult Get() => Ok(_metrics.returnMetrics());
    }
}