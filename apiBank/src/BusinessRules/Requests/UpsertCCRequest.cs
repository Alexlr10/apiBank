namespace apiBank.src.BusinessRules.Requests
{
    public class UpsertCCRequest
    {
        public Guid? Id { get; set; }
        public string Conta { get; set; }
        public double Saldo { get; set; }
    }
}
