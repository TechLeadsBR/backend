using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Internal;
using Talentos.Senai.Repositories;

namespace Talentos.Senai.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private UploadImagesRepository _uploadImagesRepository;


        public UploadController(HostingEnvironment hostingEnvironment)
        {
            _uploadImagesRepository = new UploadImagesRepository();
        }

        [HttpPost]
        public IActionResult UploadImage()
        {
            // Save image

            try
            {
                var file = Request.Form.Files[0];
                _uploadImagesRepository.Upload(file, "StudentImages");

                return Ok("Deu bom");
            }
            catch (FileLoadException error)
            {
                Console.WriteLine("COMENTARIO DEU RUIM");
                Console.WriteLine(error);
                return BadRequest("Deu ruim");
            }


            //var files = HttpContext.Request.Form.Files;

            //if (files.Count != 0)
            //{
            //    var ImagePath = @"Images\";
            //    var Extension = Path.GetExtension(files[0].FileName);
            //    var RelativeImagePath = ImagePath + files[0].FileName;
            //    var AbsImagePath = Path.Combine("rootpah", RelativeImagePath);

               

            //    // upload
            //    using (var filetream = new FileStream(RelativeImagePath, FileMode.Create))
            //    {
            //        files[0].CopyTo(filetream);
            //    }

            //    return Ok("Deu bom");
            //}

            //return BadRequest("Não deu bom");
        }

    }
}
