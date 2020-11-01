using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;
using Talentos.Senai.Utilities;

namespace Talentos.Senai.Repositories
{
    public class UploadImagesRepository : IUploadImages
    {
        private readonly Functions _functions;
        private AlunoRepository _alunoRepository;
        private EmpresaRepository _empresaRepository;

        public UploadImagesRepository()
        {
            _functions = new Functions();
            _alunoRepository = new AlunoRepository();
            _empresaRepository = new EmpresaRepository();
        }

        public TypeMessage SetPathNameImageUser(string roleUser, int jtiUser, string pathname)
        {
            if (roleUser == Users.Company)
            {
                using (TalentosContext ctx = new TalentosContext())
                {
                    try
                    {
                        Empresa empresaBuscada = _empresaRepository.BuscarPorId(jtiUser);
                        empresaBuscada.NomeFoto = pathname;

                        ctx.Empresa.Update(empresaBuscada);
                        ctx.SaveChanges();

                        string okMessage = _functions.defaultMessage("upload imagem", "ok");
                        return _functions.replyObject(okMessage, true);

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error);
                        string errorMessage = _functions.defaultMessage("upload imagem", "error");
                        return _functions.replyObject(errorMessage, false);
                    }
                }
            }
            if (roleUser == Users.Student)
            {
                using (TalentosContext ctx = new TalentosContext())
                {
                    try
                    {
                        Aluno alunoBuscado = _alunoRepository.BuscarPorId(jtiUser);
                        alunoBuscado.NomeFoto = pathname;
                        ctx.Aluno.Update(alunoBuscado);
                        ctx.SaveChanges();

                        string okMessage = _functions.defaultMessage("aluno", "ok");
                        return _functions.replyObject(okMessage, true);

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error);
                        string errorMessage = _functions.defaultMessage("empresa", "error");
                        return _functions.replyObject(errorMessage, false);
                    }
                }
            }
            else
            {
                string notFoundMessage = _functions.defaultMessage("tipo usuario", "notfound");
                return _functions.replyObject(notFoundMessage, false);
            }
        }

        public TypeMessage SaveImage(IFormFile file, string savingFolder)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Images", savingFolder);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                string okMessage = _functions.defaultMessage("upload imagens", "ok");
                return _functions.replyObject(okMessage, true);
            }
            else
            {
                string errorMessage = _functions.defaultMessage("upload imagens", "error");
                return _functions.replyObject(errorMessage, false);
            };
        }
    }
}
