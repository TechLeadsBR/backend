using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Talentos.Senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly HostingEnvironment _hostingEnvironment;


        public UploadController(HostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult UploadImage()
        {
            // Save image

            string wwwroothPath = _hostingEnvironment.WebRootPath;

            var files = HttpContext.Request.Form.Files;

            if (files.Count != 0)
            {
                var ImagePath = @"Images\";
                var Extension = Path.GetExtension(files[0].FileName);
                Console.WriteLine(Extension);
                var RelativeImagePath = ImagePath + files[0].FileName;
                var AbsImagePath = Path.Combine("rootpah", RelativeImagePath);

                // upload
                using (var filetream = new FileStream(RelativeImagePath, FileMode.Create))
                {
                    files[0].CopyTo(filetream);
                }

                return Ok("Deu bom");
            }

            return BadRequest("Não deu bom");
        }

    }
}
