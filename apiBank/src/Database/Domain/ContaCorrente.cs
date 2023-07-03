namespace apiBank.src.Database.Domain
{
    public class ContaCorrente
    {
        public Guid? Id { get; set; }
        public string Conta { get; set; }
        public double Saldo { get; set; }
    }
}
