using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Talentos.Senai.General;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Repositories;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogin _loginRepository;

        public LoginController()
        {
            _loginRepository = new LoginRepository();
        }

        [HttpGet]
        public async Task<object> Login(LoginViewModel data)
        {
            Usuario usuarioBuscado =  _loginRepository.Usuario(data);

            // if (usuarioBuscado.aluno == null && usuarioBuscado.empresa == null) return new { message = "E-mail ou senha inválidos" };

            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            TokenClient tokenClient = new TokenClient(disco.TokenEndpoint, "client_talentos");
            
            TokenResponse tokenResponse = await tokenClient.RequestCustomAsync(new { a = "asdsad"});

            return tokenResponse;

            //if (tokenResponse.IsError)
            //{
            //    return new { 
            //        error = tokenResponse.Error
            //    };
            //}

            //Console.WriteLine(tokenResponse.Json);
            //Console.WriteLine("\n\n");

            //var client = new HttpClient();
            //client.SetBearerToken(tokenResponse.AccessToken);

            //var response = await client.GetAsync("http://localhost:5000/identity");
            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine(response.StatusCode);
            //    Console.ReadKey();
            //    return new
            //    {
            //        response = response.StatusCode
            //    };
            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content));
            //    Console.ReadKey();
            //    return new
            //    {
            //        json = content
            //    };
            //}
        }
    }
}