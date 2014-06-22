using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Login
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool RelembrarSenha { get; set; }
        public string Nome { get; set; }

        /// <summary>
        /// Virifica se o usuario existe
        /// </summary>
        /// <param name="login">Nome do Usuario</param>
        /// <param name="senha">Senha do Usuario</param>
        /// <returns>Se o usuario e senha existir retorna com verdadeiro</returns>
        public bool IsValid(string login, string senha)
        {
            var usuarios = new Usuarios();

            var usuario = usuarios.ObterAcesso(login, senha);

            //Melhorar
            Nome = usuario != null ?  usuario.Nome : string.Empty;

            return usuario != null ;
        }
    }
}