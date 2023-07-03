using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;
using apiBank.src.Database.Repositories.Interfaces;

namespace apiBank.src.BusinessRules.Handlers
{
    public class DepositarContaHandler : IDepositarContaHandler
    {
        private readonly IContaCorrenteRepository _repository;

        public DepositarContaHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public UpsertCCResponse Execute(MovimentarContaRequest request)
        {
            var conta = _repository.GetByConta(request.Conta);
            if (conta == null)
            {
                throw new Exception("Conta não encontrada");
            }

            conta.Saldo += request.Valor;

            _repository.Atualizar(conta);

            return new UpsertCCResponse
            {
                Payload = new UpsertCCResponsePayload
                {
                    Id = conta.Id.Value,
                    Conta = conta.Conta,
                    Saldo = conta.Saldo
                }
            };
        }
    }
}
