using Gestao_API.Models;
using Gestao_API.Persistence;
using Gestao_API.Regras;
using Gestao_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao_API.Regras
{
    /// <summary>
    /// CLASSE ONDE FICA ARMAZENADA A LÓGICA DAS FUNCIONALIDADES DA API.
    /// COMO DEPOSITAR, CONSULTAR EXTRATO E TRANSFERENCIA.
    /// </summary>
    public class TransacaoNegocio
    {
        private readonly UsuarioNegocio _usuario;
        private readonly GestaoDbContext _db;
        public TransacaoNegocio(UsuarioNegocio usuario, GestaoDbContext db)
        {
            _usuario = usuario;
            _db = db;
        }

        /// <summary>
        /// Método para depositar um valor na conta de um usuário.
        /// Passando o UsuarioID para a localização do usuário no banco de dados e valor para armazenar o valor do depósito.
        /// </summary>

        public IActionResult Depositar(string UsuarioID, decimal valor)
        {

            if (ValidarConta(UsuarioID))
            {
                Transacao transacao = new Transacao();
                transacao.Valor = valor;
                transacao.UsuarioId = Guid.Parse(UsuarioID);
                transacao.HoraLancamento = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                transacao.TransacaoId = new Guid();
                transacao.TransferenciaId = new Guid();

                _db.Entry(transacao).State = EntityState.Added;
                _db.SaveChanges();
                return new OkObjectResult("Deposito efetuado!");

            }

            return new BadRequestObjectResult("Falha na autenticação, conta nao encontrada");
        }


        /// <summary>
        /// Método para consultar o saldo de uma conta de um usuário.
        /// Busca na tabela de Transacoes os locais onde o ID do usuário está presente.
        /// Percorre a lista de transacoes, faz o cálculo dos valor e o retorna.
        /// </summary>
        public decimal ConsultarSaldo(string UsuarioId)
        {
            List<Transacao> consulta = _db.Transacoes.Where(x => x.UsuarioId == Guid.Parse(UsuarioId)).ToList();
            decimal saldo = consulta.Sum(x => x.Valor);
            return saldo;
        }

        /// <summary>
        /// Método para realizar uma transferencia de valor entre contas.
        /// É necessário informar o Id do remetente, o valor que deseja ser transferido e o Id da conta destinatária.
        /// Verifica se o remetente possui o valor em conta, valida as duas contas e aplica uma lógica para a transferencia.
        /// Salva as alterações no banco.
        /// </summary>
        public IActionResult Transferencia(string UsuarioId, decimal Valor, string UsuarioTransferenciaId)
        {
            var transferenciaID = Guid.NewGuid();
            decimal SaldoConta = ConsultarSaldo(UsuarioId);

            if (SaldoConta > Valor)
            {
                if (ValidarConta(UsuarioId) && ValidarConta(UsuarioTransferenciaId))
                {
                    Transacao transacao1 = new Transacao();

                    transacao1.UsuarioId = Guid.Parse(UsuarioId);
                    transacao1.Valor = transacao1.Valor - Valor; // é subtraido da conta do remetende.
                    transacao1.HoraLancamento = DateTime.UtcNow;
                    transacao1.TransacaoId = Guid.NewGuid();
                    transacao1.TransferenciaId = transferenciaID;
                    _db.Add(transacao1);
                    _db.SaveChanges();

                    Transacao transacao2 = new Transacao();

                    transacao2.UsuarioId = Guid.Parse(UsuarioTransferenciaId);
                    transacao2.Valor = transacao2.Valor + Valor; // é adicionado a conta do destinatário.
                    transacao2.HoraLancamento = DateTime.UtcNow;
                    transacao2.TransacaoId = Guid.NewGuid();
                    transacao2.TransferenciaId = transferenciaID;
                    _db.Add(transacao2);
                    _db.SaveChanges();

                    return new OkObjectResult("Transacao efetuada com sucesso!");
                }
            }
            return new BadRequestObjectResult("Informe uma conta válida!");
        }

        /// <summary>
        /// Obtem todo o histórico de transações da conta pertencente ao ID informado através de uma consulta no banco de dados.
        /// </summary>
        public List<Transacao> ObterExtrato(string UsuarioId)
        {
            var consulta = _db.Transacoes.Where(x => x.UsuarioId == Guid.Parse(UsuarioId)).ToList();
            return consulta;
        }

        /// <summary>
        /// Utiliza o método para procurar e retornar um usuario cadastrado no banco de dados.
        /// Verifica se as propriedades Nome, Email e Senha de usuarioObtido não são nulas ou vazias.
        /// A expressão retorna true se todas essas propriedades não forem nulas ou vazias e false caso contrário.
        /// </summary>
        public bool ValidarConta(string usuarioID)
        {
            var usuarioObtido = _usuario.ObterUsuario(usuarioID);
            return !string.IsNullOrEmpty(usuarioObtido.Nome) && !string.IsNullOrEmpty(usuarioObtido.Email) && !string.IsNullOrEmpty(usuarioObtido.Senha);
        }
    }
}

