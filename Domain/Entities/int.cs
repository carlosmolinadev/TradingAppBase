using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    public class @int
    {
        public @int(decimal balance, decimal riskPercentage, string currency, int exchangeId, string derivate)
        {
            Balance = balance;
            RiskPercentage = riskPercentage;
            Currency = currency;
            ExchangeId = exchangeId;
            Derivate = derivate;
        }

        public @int() { }

        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal RiskPercentage { get; set; }
        public string Currency { get; set; }
        public int ExchangeId { get; set; }
        public string Derivate { get; set; }

    }
}
