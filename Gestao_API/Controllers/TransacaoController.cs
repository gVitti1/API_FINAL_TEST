using Gestao_API.Constans;
using Gestao_API.Models;
using Gestao_API.Regras;
using Gestao_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_API.Controllers
{
    /// <summary>
    /// CONTROLLER QUE CONTEM OS ENDPOINTS DAS FUNCIONALIDADES DA API.
    /// ENDPOINTS AUTORIZADOS APENAS COM O JWT GERADO QUANDO O USUARIO FAZ LOGIN.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.AuthUsuario)] 
    public class TransacaoController : ControllerBase
    {
        /// <summary>
        /// Inejeção de dependencia da classe TransacaoNegocio para ter acesso aos seus membros.
        /// </summary>
        private readonly TransacaoNegocio _transacao;

        public TransacaoController(TransacaoNegocio transacao)
        {
            _transacao = transacao;
        }

        /// <summary>
        /// Endpoinr para realizar um depósito.
        /// invoca um método da classe TransacaoNegocio passando os parâmetros UsuarioId e Valor.
        /// </summary>
        [HttpPost("/depositar")]
        public IActionResult Depositar(string UsuarioID, decimal valor)=>
            _transacao.Depositar(UsuarioID, valor);

        /// <summary>
        /// Endpoint para consultar o saldo, que invoca um método da classe TransacaoNegocio.
        /// Passando os parâmetros UsuarioId e Valor.
        /// </summary>
        [HttpGet("/saldo")]
        public IActionResult Saldo (string Id)
        {
            decimal operacao = _transacao.ConsultarSaldo(Id);
            return Ok(operacao);
        }

        /// <summary>
        /// Endpoint transferencia de valores entre contas, que invoca um método da classe TransacaoNegocio.
        /// Passando a conta que enviará o valor, o valor em si, e a conta destino.
        /// </summary>
        [HttpGet("/transferir")]
        public IActionResult Transferir(string UsuarioId, decimal valor, string UsuarioTransferenciaId)=>
            _transacao.Transferencia(UsuarioId, valor, UsuarioTransferenciaId);


        /// <summary>
        /// Endpoint para visualização do extrato bancário.
        /// invoca um método da classe TransacaoNegocio, passando a conta que enviará o valor, o valor em si, e a conta destino.
        /// </summary>
        [HttpGet("/extrato")]
        public IActionResult Extrato(string Id)
        {
            List<Transacao> operacao = _transacao.ObterExtrato(Id);
            return Ok(operacao);
        }

    }
}
