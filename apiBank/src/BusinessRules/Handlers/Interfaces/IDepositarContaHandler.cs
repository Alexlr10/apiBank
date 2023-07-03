using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;

namespace apiBank.src.BusinessRules.Handlers.Interfaces
{
    public interface IDepositarContaHandler
    {
        UpsertCCResponse Execute(MovimentarContaRequest request);
    }
}
