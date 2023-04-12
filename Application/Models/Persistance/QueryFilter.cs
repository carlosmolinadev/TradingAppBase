namespace Application.Models.Persistance
{
    public class QueryFilter
    {
        public ICollection<QueryCondition> Condition { get; set; }
        public ICollection<OrderByColumn>? OrderByColumn { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}
