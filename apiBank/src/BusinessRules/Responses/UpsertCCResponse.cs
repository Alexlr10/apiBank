using FluentValidation.Results;

namespace apiBank.src.BusinessRules.Responses
{
    public class UpsertCCResponse
    {
        public UpsertCCResponsePayload Payload { get; set; }
        public List<ValidationFailure>? Errors { get; set; }
    }

    public class UpsertCCResponsePayload 
    {
        public Guid? Id { get; set; }
        public string Conta { get; set; }
        public double Saldo { get; set; }
    }

}
