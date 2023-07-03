using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;

namespace apiBank.src.Api
{
    public class Query
    {
        public ListasDeContasResponses GetContas([Service] IGetAllCCHandler handler)
        {
            return handler.Execute();
        }

        public ContaResponse GetConta([Service] IGetByIdCCHandler handler, GetByIdCCRequest request)
        {
            return handler.Execute(request);
        }

        public ContaResponse GetSaldo([Service] IGetByContaVerSaldoHandler handler, GetByContaVerSaldoRequest request)
        {
            return handler.Execute(request);
        }
    }
}
