using System;
using Microsoft.AspNetCore.Mvc;
using Talentos.Senai.Repositories;
using Microsoft.AspNetCore.Authorization;
using Talentos.Senai.Utilities;
using Microsoft.AspNetCore.Http;
using Talentos.Senai.Interfaces;

namespace Talentos.Senai.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IUploadImages _uploadImagesRepository;
        private Functions _functions;

        public UploadController()
        {
            _uploadImagesRepository = new UploadImagesRepository();
            _functions = new Functions();
        }

        [Authorize(Roles = Users.Student + "," + Users.Company)]
        [HttpPost]
        public IActionResult UploadImage()
        {
            var token = Request.Headers["Authorization"][0].Split(" ")[1];
            string jtiUser = _functions.GetClaimInBearerToken(token, "jti");
            string roleUser = _functions.GetClaimInBearerToken(token, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            string nameFolder = roleUser == Users.Student ? "StudentImages" : "CompanyImages";

            IFormFile file = Request.Form.Files[0];
            TypeMessage uploadRepository = _uploadImagesRepository.SaveImage(file, nameFolder);
            if (uploadRepository.ok)
            {
                string path = nameFolder + "/" + file.FileName.ToString();
                TypeMessage saved = _uploadImagesRepository.SetPathNameImageUser(roleUser, Convert.ToInt32(jtiUser), path);
                if (saved.ok)
                {
                    return StatusCode(201, uploadRepository);
                } else
                {
                    return BadRequest(saved);
                }
            }
            else return BadRequest(uploadRepository);
        }
    }
}
