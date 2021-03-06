﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Talentos.Senai.Domains;
using Talentos.Senai.Interfaces;
using Talentos.Senai.Utilities;
using Talentos.Senai.ViewModels;

namespace Talentos.Senai.Repositories
{
    public class LoginRepository : ILogin
    {
        private readonly Functions _functions;
        private string _notFoundUser;

        public LoginRepository()
        {
            _functions = new Functions();
            _notFoundUser = "E-mail ou senha inválidos";
        }

        public TypeMessage BuscarAluno(LoginViewModel data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                try
                {
                    Aluno alunoBuscado = ctx.Aluno.FirstOrDefault(a => a.Email == data.Email && a.Senha == data.Senha);

                    if (alunoBuscado != null)
                    {
                        var token = CreateToken(alunoBuscado.Email, alunoBuscado.IdAluno.ToString(), alunoBuscado.IdTipoUsuario.ToString());
                        return _functions.replyObject(token.ToString(), true);
                    }
                    else
                    {
                        string okMessage = "E-mail ou senha inválidos";
                        return _functions.replyObject(okMessage, false);
                    }
                } catch(Exception error)
                {
                    Console.WriteLine(error);
                    string errorMessage = "Erro no procedimento";
                    return _functions.replyObject(errorMessage, false);
                }
            }
        }

        public TypeMessage BuscarEmpresa(LoginViewModel data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                Empresa empresaBuscada = ctx.Empresa.FirstOrDefault(e => e.Email == data.Email && e.Senha == data.Senha);

                if (empresaBuscada != null)
                {
                    var token = CreateToken(empresaBuscada.Email, empresaBuscada.IdEmpresa.ToString(), empresaBuscada.IdTipoUsuario.ToString());
                    return _functions.replyObject(token.ToString(), true);
                }
                else
                {
                    return _functions.replyObject(_notFoundUser, false);
                }
            }
        }

        public TypeMessage BuscarAdministrador(LoginViewModel data)
        {
            using (TalentosContext ctx = new TalentosContext())
            {
                Administrador administradorBuscado = ctx.Administrador.FirstOrDefault(a => a.Email == data.Email && a.Senha == data.Senha);
                if (administradorBuscado != null)
                {
                    var token = CreateToken(administradorBuscado.Email, administradorBuscado.IdAdministrador.ToString(), administradorBuscado.IdTipoUsuario.ToString());
                    return _functions.replyObject(token.ToString(), true);
                }
                else
                {
                    return _functions.replyObject(_notFoundUser, false);
                }
            }
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
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
