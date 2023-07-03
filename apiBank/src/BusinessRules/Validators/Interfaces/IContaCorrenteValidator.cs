using apiBank.src.BusinessRules.Requests;
using FluentValidation;

namespace apiBank.src.BusinessRules.Validators.Interfaces
{
    public interface IContaCorrenteValidator : IValidator<UpsertCCRequest>
    {

    }
}
