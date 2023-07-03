using apiBank.src.Database.Domain;
using apiBank.src.Database.Repositories.Interfaces;

namespace apiBank.src.Database.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly BankContext _db;
        public ContaCorrenteRepository(BankContext db)
        {
            _db = db;
        }
        public IQueryable<ContaCorrente> GetAll()
        {
            return _db.Conta.AsQueryable();
        }
        public ContaCorrente GetById(Guid id)
        {
            return _db.Conta.AsQueryable().SingleOrDefault(c => c.Id == id);
        }
        public ContaCorrente GetByConta(string numeroConta)
        {
            return _db.Conta.FirstOrDefault(c => c.Conta == numeroConta);
        }

        public ContaCorrente Save(ContaCorrente conta)
        {
            if (!conta.Id.HasValue)
            {
                conta.Id = Guid.NewGuid();
                _db.Conta.Add(conta);
            }

            _db.SaveChanges();
            return conta;
        }

        public ContaCorrente Atualizar(ContaCorrente conta)
        {
            _db.Conta.Update(conta);
            _db.SaveChanges();
            return conta;
        }
    }
}
