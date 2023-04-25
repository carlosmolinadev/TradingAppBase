namespace Application.Models.Persistance
{
    public class QueryCondition
    {
        public QueryCondition(string column, string operatorSign, string value) { Column = column; Operator = operatorSign; Value = value; }
        public QueryCondition(string column, string value) { Column = column; Value = value; }
        public string Column { get; set; }
        public string Operator { get; set; } = "=";
        public string Value { get; set; }
    }
}
