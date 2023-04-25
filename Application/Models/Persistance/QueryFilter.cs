namespace Application.Models.Persistance
{
    public class QueryFilter
    {
        public IList<QueryCondition> Condition { get; set; } = new List<QueryCondition>();
        public IList<string> OrderByColumn { get; set; } = new List<string>();
        public string Direction { get; private set; } = string.Empty;
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        public QueryFilter() {}

        public QueryFilter(string column, string operatorSign, string value, int limit, int offset)
        {
            AddCondition(column, operatorSign, value);
            Limit = limit;
            Offset = offset;
        }
        public QueryFilter(string column, string operatorSign, string value, int limit)
        {
            AddCondition(column, operatorSign, value);
            Limit = limit;
        }

        public void AddCondition(string column, string operatorSign, string value)
        {
            var queryCondition = new QueryCondition() { Column = column, Operator = operatorSign, Value = value };
            Condition.Add(queryCondition);
        }

        public void AddEqualCondition(string column, string value)
        {
            var queryCondition = new QueryCondition() { Column = column, Operator = "=", Value = value };
            Condition.Add(queryCondition);
        }

        public void AddOrderParameter(string column)
        {
            OrderByColumn.Add(column);
        }

        public void SetOrderDesc()
        {
            Direction = "desc";
        }

    }
}
