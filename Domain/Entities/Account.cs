using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal RiskPerTrade { get; set; }
        public string? Currency { get; set; }
        public Exchange Exchange { get; set; }
        public Derivate Derivate { get; set; }
        [NotMapped]
        public IEnumerable<Trade> Trades { get; set; } = Enumerable.Empty<Trade>();
    }
}
