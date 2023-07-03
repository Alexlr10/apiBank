using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;
using apiBank.src.BusinessRules.Validators.Interfaces;
using apiBank.src.Database.Domain;
using apiBank.src.Database.Repositories.Interfaces;

namespace apiBank.src.BusinessRules.Handlers
{
    public class UpsertCCHandler : IUpsertCCHandler
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IContaCorrenteValidator _validator;

        public UpsertCCHandler(IContaCorrenteRepository repository, IContaCorrenteValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public UpsertCCResponse Execute(UpsertCCRequest request)
        {
            var validatorResult = _validator.Validate(request);
            if (!validatorResult.IsValid)
            {
                return new UpsertCCResponse
                {
                    Errors = validatorResult.Errors
                };
            }
            ContaCorrente contaEntity;

            if (request.Id.HasValue)
            {
                contaEntity = _repository.GetById(request.Id.Value);
                if (contaEntity == null)
                {
                    throw new Exception("Conta não encontrada");
                }
                if (contaEntity.Conta == request.Conta)
                {
                    throw new Exception("Número da conta já existente");
                }
            }
            else
            {
                contaEntity = new ContaCorrente();
            }

            contaEntity.Conta = request.Conta;
            contaEntity.Saldo = request.Saldo;
            contaEntity = _repository.Save(contaEntity);

            return new UpsertCCResponse
            {
                Payload = new UpsertCCResponsePayload
                {
                    Id = contaEntity.Id.Value,
                    Conta = contaEntity.Conta,
                    Saldo = contaEntity.Saldo
                }
            };
        }
    }
}

