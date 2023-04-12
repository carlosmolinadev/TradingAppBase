using Domain.Entities;

namespace Application.Profiles.DTOs
{
    public class GridParameter
    {
        public Pair? GridLow { get; set; }
        public Pair? GridHigh { get; set; }
        public decimal? RiskFactor { get; set; }
        public int GridNumber { get; set; }
        public MarketStructure GridType { get; set; }
        public IList<OrderParameter> GridOrders { get; set;}
    }
}
