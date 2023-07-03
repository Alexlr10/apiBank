using System.Diagnostics.CodeAnalysis;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Validators.Interfaces;
using FluentValidation;

namespace apiBank.src.BusinessRules.Validators
{
    [ExcludeFromCodeCoverage]
    public class ContaCorrenteValidator : AbstractValidator<UpsertCCRequest>, IContaCorrenteValidator
    {
        public ContaCorrenteValidator()
        {
            RuleFor(request => request.Conta)
                .NotEmpty().WithMessage("O número da conta é obrigatório.")
                .MaximumLength(15).WithMessage("O número da conta deve ter no máximo 15 caracteres.");

            RuleFor(request => request.Saldo)
                .NotEmpty().WithMessage("O saldo da conta é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O saldo da conta deve ser maior ou igual a zero.");
        }
    }
}
