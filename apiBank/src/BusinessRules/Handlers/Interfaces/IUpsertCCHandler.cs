using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;

namespace apiBank.src.BusinessRules.Handlers.Interfaces
{
    public interface IUpsertCCHandler
    {
        UpsertCCResponse Execute(UpsertCCRequest request);
    }
}
