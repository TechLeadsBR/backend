using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.General;
using Talentos.Senai.Interfaces;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Repositories
{
    public class LoginRepository : ILogin
    {
        private TalentosContext ctx = new TalentosContext();

        public Aluno BuscarAluno(LoginViewModel data) => ctx.Aluno.FirstOrDefault(a => a.Email == data.email && a.Senha == data.senha);

        public Empresa BuscarEmpresa(LoginViewModel data) => ctx.Empresa.FirstOrDefault(e => e.Email == data.email && e.Senha == data.senha);

        public Usuario BuscarUsuario(LoginViewModel data)
        {
            return new Usuario
            {
                aluno = BuscarAluno(data),
                empresa = BuscarEmpresa(data)
            };
        }

        public object CreateToken(string email, string idUsuario, string tipoUsuario)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, idUsuario.ToString()),
                new Claim(ClaimTypes.Role, tipoUsuario.ToString())
            };
            

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("talentos.key.autentication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "talentos.senai.api",
                audience: "talentos.senai.api",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
