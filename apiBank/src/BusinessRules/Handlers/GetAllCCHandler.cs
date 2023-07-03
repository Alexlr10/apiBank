using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Responses;
using apiBank.src.Database.Repositories.Interfaces;

namespace apiBank.src.BusinessRules.Handlers
{
    public class GetAllCCHandler : IGetAllCCHandler
    {
        private readonly IContaCorrenteRepository _repository;

        public GetAllCCHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public ListasDeContasResponses Execute()
        {
           var conta = _repository.GetAll()
                .Select(c => new ContaResponseItem 
                { 
                    Id = c.Id,
                    Conta = c.Conta,
                    Saldo = c.Saldo,
                }).ToList();

            return new ListasDeContasResponses
            {
                Payload = conta
            };
        }
    }
}
