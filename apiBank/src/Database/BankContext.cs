using apiBank.src.Database.Domain;
using Microsoft.EntityFrameworkCore;

namespace apiBank.src.Database
{
    public class BankContext : DbContext
    {
        public BankContext()
        {
        }

        public BankContext(DbContextOptions options) : base(options)
        {
            // Carregar as variáveis de ambiente do arquivo .env
            DotNetEnv.Env.Load();
        }

        public DbSet<ContaCorrente> Conta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
            string database = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "bancodigital";
            string user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "91851007";

            string connectionString = $"server={server};database={database};user={user};password={password}";

            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("8.0.31-mysql"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaCorrente>(entity =>
            {
                entity.ToTable("ContaCorrente"); // Define o nome da tabela no banco de dados (opcional)
                entity.HasKey(e => e.Id); // Define a chave primária da entidade
                entity.Property(e => e.Conta).IsRequired(); // Define a propriedade 'Conta' como obrigatória
                entity.Property(e => e.Saldo).IsRequired(); // Define o tipo e precisão da propriedade 'Saldo'
            });
        }
    }
}
