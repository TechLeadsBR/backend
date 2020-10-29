using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace Talentos.Senai.Repositories
{
    public class UploadImagesRepository
    {
            
        public TypeMessage SaveImage(IFormFile arquivo, string savingFolder)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), savingFolder);

            if (arquivo.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    arquivo.CopyTo(stream);
                }

                string okMessage = null;

                return savingFolder + "/" + fileName;
            }
            else
            {
                return null;
            }
        }
    }
}
