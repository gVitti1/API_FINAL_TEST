using Gestao_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao_API.Persistence
{
    /// <summary>
    /// CRIAÇÃO DO CONTEXTO DO BANCO DE DADOS E A PREPARAÇÃO PARA APLICAR MIGRATIONS.
    /// </summary>
    public class GestaoDbContext : DbContext
    {
        public GestaoDbContext(DbContextOptions<GestaoDbContext> options):base(options)
        {      
            
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
