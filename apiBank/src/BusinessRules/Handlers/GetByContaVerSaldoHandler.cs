using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;
using apiBank.src.Database.Repositories.Interfaces;

namespace apiBank.src.BusinessRules.Handlers
{
    public class GetByContaVerSaldoHandler : IGetByContaVerSaldoHandler
    {
        private readonly IContaCorrenteRepository _repository;

        public GetByContaVerSaldoHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public ContaResponse Execute(GetByContaVerSaldoRequest request)
        {
            var conta = _repository.GetByConta(request.Conta);
            if (conta == null)
            {
                throw new Exception("Conta não encontrada");
            }

            return new ContaResponse
            {
                Payload = new ContaResponseItem
                {
                    Id = conta.Id,
                    Conta = conta.Conta,
                    Saldo = conta.Saldo
                }
            };
        }
    }
}
