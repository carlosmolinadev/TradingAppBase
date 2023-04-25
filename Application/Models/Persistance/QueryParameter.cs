namespace Application.Models.Persistance
{
    public class QueryParameter
    {
        public IList<QueryCondition> Condition { get; set; } = new List<QueryCondition>();
        public IList<string> OrderByColumn { get; set; } = new List<string>();
        public string Direction { get; private set; } = string.Empty;
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        public QueryParameter() {}

        public QueryParameter(string column, string value)
        {
            AddEqualCondition(column, value);
        }

        public void AddCondition(string column, string operatorSign, string value)
        {
            var queryCondition = new QueryCondition(column, operatorSign, value);
            Condition.Add(queryCondition);
        }

        public QueryParameter(string column, string operatorSign, string value, int limit)
        {
            AddCondition(column, operatorSign, value);
            Limit = limit;
        }

        public QueryParameter(string column, string operatorSign, string value, int limit, int offset)
        {
            AddCondition(column, operatorSign, value);
            Limit = limit;
            Offset = offset;
        }

        public void AddEqualCondition(string column, string value)
        {
            var queryCondition = new QueryCondition(column, value);
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
