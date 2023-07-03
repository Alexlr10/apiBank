using apiBank.src.Database.Domain;

namespace TestApiBank.Data
{
    public class ContaCorrenteData
    {
        public static IQueryable<ContaCorrente> GetContas()
        {
            var contas = new List<ContaCorrente>
            {
                new ContaCorrente { Id = Guid.Parse("34B3B9F6-9952-48CC-AB50-3CEA740F2F3C"), Conta = "0001", Saldo = 1000.0 },
                new ContaCorrente { Id = Guid.Parse("E8029657-39C0-465A-822B-5D7A7A5DE3B2"), Conta = "0002", Saldo = 2000.0 },
                new ContaCorrente { Id = Guid.Parse("9598502D-CC5B-4E39-8F38-D0692BD93003"), Conta = "0003", Saldo = 3000.0 }
            };

            return contas.AsQueryable();
        }
    }
}
