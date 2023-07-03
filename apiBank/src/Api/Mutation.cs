using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;

namespace apiBank.src.Api
{
    public class Mutation
    {
        public UpsertCCResponse UpsertConta([Service] IUpsertCCHandler handler, UpsertCCRequest request)
        {
            return handler.Execute(request);
        }

        public UpsertCCResponse SaqueEmConta([Service] ISacarContaHandler handler, MovimentarContaRequest request)
        {
            return handler.Execute(request);
        }

        public UpsertCCResponse DepositoEmConta([Service] IDepositarContaHandler handler, MovimentarContaRequest request)
        {
            return handler.Execute(request);
        }
    }
}
