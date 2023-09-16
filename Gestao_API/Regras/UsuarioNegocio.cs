using Gestao_API.Models;
using Gestao_API.Persistence;
using Gestao_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Gestao_API.Regras
{
    /// <summary>
    /// CLASSE ONDE FICA ARMAZENADA A LÓGICA DE CADASTRO E LOGIN DE USUARIO.
    /// </summary>
    public class UsuarioNegocio
    {
        private readonly GestaoDbContext _db;

        public UsuarioNegocio(GestaoDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Método para cadastrar um novo usuario, passando um nome, um email e uma senha.
        /// Cria um novo Usuario, gera um novo ID, e com os valores informados acessa o banco e o grava na tabela de Usuarios.
        /// Retorna uma string informando o ID gerado.
        /// </summary>
        public IActionResult Cadastar(string nome, string email, string senha)
        {
            // Verifica se o email já existe no banco de dados
            if (_db.Usuarios.Any(u => u.Email == email))
            {
                return new BadRequestObjectResult("Email já cadastrado.");
            }
            Usuario novoUsuario = new Usuario();
            novoUsuario.UsuarioId = new Guid();
            novoUsuario.Nome = nome;
            novoUsuario.Email = email;
            novoUsuario.Senha = senha;

            _db.Entry(novoUsuario).State = EntityState.Added;
            _db.SaveChanges();

            return new OkObjectResult("Sucesso na criação de usuario" + "seu ID de usuario é: " + novoUsuario.UsuarioId);
        }

        /// <summary>
        ///  Método de autenticação que recebe um email e uma senha.
        ///  busca e compara no banco pelo email e senha inseridos. Se ambos estiverem corretos,
        ///  retorna um Token de acesso em forma de objeto.
        /// </summary>
        public IActionResult AutenticarUsuario(string email, string senha)
        {
            var usuario = _db.Usuarios.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (usuario == null)
                return new BadRequestObjectResult(new { error = "Usuário não encontrado" });

            if (senha == usuario.Senha)
            {
                var token = TokenService.GenerateToken(usuario);
                return new OkObjectResult (token);
            }

            return new BadRequestObjectResult(new {error = "Senha incorreta" });
        }

        /// <summary>
        /// Método para obter um usuário especifico cadastrado no Banco de dados, 
        /// ele recebe como parametro o Identificador da conta que será exibida.
        /// Retorna o usuario encontrado.
        /// </summary>
        public Usuario ObterUsuario(string id)
        {
            var usuario = new Usuario();

            var usuarioEncontrado = _db.Usuarios.FirstOrDefault(x => x.UsuarioId.ToString() == id);
            usuario.Email = usuarioEncontrado.Email;
            usuario.Senha = usuarioEncontrado.Senha;
            usuario.Nome = usuarioEncontrado.Nome;
            usuario.UsuarioId = usuarioEncontrado.UsuarioId;

            return usuario;
        }
    }
}
