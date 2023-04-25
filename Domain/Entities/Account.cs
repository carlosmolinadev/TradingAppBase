using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Account
    {
        public Account(int id, decimal balance, decimal riskPercentage, string currency, Exchange exchange, string derivate)
        {
            Id = id;
            Balance = balance;
            RiskPercentage = riskPercentage;
            Currency = currency;
            Exchange = exchange;
            Derivate = derivate;
        }
        public Account(Exchange exchange, string derivate, string currency)
        {
            Exchange = exchange;
            Derivate = derivate;
            Currency = currency;
        }

        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal RiskPercentage { get; set; }
        public string Currency { get; set; }
        public Exchange Exchange { get; set; }
        public string Derivate { get; set; }
        [NotMapped]
        public IList<Trade> Trades { get; set; }  = new List<Trade>();
    }
}
