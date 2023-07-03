using apiBank.src.Database.Domain;

namespace apiBank.src.Database.Repositories.Interfaces
{
    public interface IContaCorrenteRepository
    {
        IQueryable<ContaCorrente> GetAll();
        ContaCorrente GetById(Guid id);
        ContaCorrente GetByConta(string numeroConta);
        ContaCorrente Save(ContaCorrente conta);
        ContaCorrente Atualizar(ContaCorrente conta);
    }
}
