using Gestao_API.Constans;
using Gestao_API.Models;
using Gestao_API.Persistence;
using Gestao_API.Regras;
using Gestao_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;

namespace Gestao_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    /// CONTROLLER QUE CONTEM AS FUNCIONALIDADES REFERENTES A USUARIO.
    /// O ACESSO A ESSE CONTROLLER NÃO NECESSITA DE AUTENTICAÇÃO POR JWT.
    /// </summary>
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Inejeção de dependencia da classe UsuarioNegocio para ter acesso aos seus componentes.
        /// </summary>
        private readonly UsuarioNegocio _usuario;

        public UsuarioController(UsuarioNegocio usuario)
        {
            _usuario = usuario;
        }

        /// <summary>
        /// Endpoint para cadastrar um usuario, recebu um nome, um email e uma senha.
        ///  Os usa como parâmetros para o método Cadastrar da classe Usuarionegocio.
        /// </summary>
        [HttpPost("/cadastro")]
        public IActionResult CadastrarUsuario(string nome, string email, string senha) =>
            _usuario.Cadastar(nome, email, senha);

        /// <summary>
        /// Endpoint de login que recebe um email e uma senha.  
        /// Os usa como parâmetros para o método AutenticarUsuario da classe UsuarioNegocio.
        /// </summary>
        [HttpGet("/login")] 
        public IActionResult Login(string email, string senha)=>
            _usuario.AutenticarUsuario(email, senha);
    }
}
