using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talentos.Senai.Interfaces
{
    interface IUploadImages
    {
        TypeMessage SetPathNameImageUser(string roleUser, int jtiUser, string pathname);

        TypeMessage SaveImage(IFormFile file, string savingFolder)
    }
}
