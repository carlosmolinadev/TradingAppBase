using Application.Contracts.Persistance;
using Application.Models.Persistance;
using Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Reflection;


namespace Infrastructure.Persistance.Repositories
{
    
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly string _tableName;
        private readonly IUnitOfWork _unitOfWork;
        

        public RepositoryAsync(IUnitOfWork unitOfWork)
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                _tableName = tableAttr.Name;
            }
            else
            {
                _tableName = ToSnakeCase(typeof(T).Name);
            }

            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> SelectByIdAsync(int id)
        {
            try
            {
                var sql = ConvertSql($"SELECT * FROM {_tableName} WHERE id = @id");
                return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<T>(
                    sql,
                    new { id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<ICollection<T>> SelectAllAsync()
        {
            try
            {
                var sql = ConvertSql($"SELECT * FROM {_tableName}");
                var result = await _unitOfWork.Connection.QueryAsync<T>(sql);
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<T>> SelectFilteredAsync(QueryFilter filter)
        {
            // Build the SQL query
            var sql = ConvertSql($"SELECT * FROM {_tableName}");
            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();
            if (filter.Condition.Count > 0)
            {
                // Add WHERE clauses for each filter condition
                var i = 0;
                foreach (var condition in filter.Condition)
                {
                    whereClauses.Add($"{ToSnakeCase(condition.Column)} {condition.Operator} @p{i}");
                    parameters.Add($"p{i}", condition.Value);
                    i++;
                }
            }
            if (whereClauses.Any())
            {
                sql += $" WHERE {string.Join(" AND ", whereClauses)}";
            }
            if (filter.OrderByColumn.Count > 0)
            {
                // Add ORDER BY clause for each OrderByColumn
                var orderByClauses = filter.OrderByColumn.Select(x => $"{x}");
                sql += $" ORDER BY {string.Join(", ", orderByClauses)}";
            }
            if (filter.Limit.HasValue)
            {
                sql += $" LIMIT {filter.Limit}";
                // Add LIMIT and OFFSET clauses for paging
                if (filter.Offset.HasValue)
                {
                    sql += $" OFFSET {filter.Offset}";
                }
                 
            }

            // Execute the query and return the results
            var result = await _unitOfWork.Connection.QueryAsync<T>(sql, parameters);
            return result.ToList();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                var p = new DynamicParameters();
                var properties = GetPropertyInfo();
                var columns = string.Join(",", properties.Where(x => x.Name != "Id").Select(x => ToSnakeCase(x.Name)));
                var values = string.Join(",", properties.Where(x => x.Name != "Id").Select(x => $"@{x.Name}"));

                foreach (var item in properties)
                {
                    p.Add(item.Name, item.GetValue(entity));
                }

                var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({values}) RETURNING *";

                return await _unitOfWork.Connection.QueryFirstAsync<T>(sql, p);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                var primaryKey = GetPrimaryKeyInfo();
                var updates = string.Join(',', GetPropertyInfo().Select(c => $"{ToSnakeCase(c.Name)} = @{c.Name}"));
                var sql = $"UPDATE {_tableName} SET {updates} WHERE id = @id";
                if (primaryKey is not null)
                {
                    sql = $"UPDATE {_tableName} SET {updates} WHERE {ToSnakeCase(primaryKey.Name)} = @{primaryKey.Name}";
                }

                return await _unitOfWork.Connection.ExecuteAsync(sql, entity) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                var primaryKey = GetPrimaryKeyInfo();
                var sql = $"DELETE FROM {_tableName} WHERE id = @id";
                if (primaryKey != null)
                {
                    sql = $"DELETE FROM {_tableName} WHERE {ToSnakeCase(primaryKey.Name)} = @{primaryKey.Name}";
                }

                return await _unitOfWork.Connection.ExecuteAsync(sql, entity) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<ICollection<U>> LoadAsync<U, P>(string storedProcedure, P parameters)
        {
            Type type = typeof(U);
            FieldInfo[] fields = type.GetFields();
            var p = new DynamicParameters();

            if (fields.Length > 0)
            {
                storedProcedure = $"SELECT {storedProcedure}";
            }
            else
            {
                storedProcedure = $"SELECT * FROM {storedProcedure}";
            }

            if (parameters is not null)
            {
                PropertyInfo[] properties;
                properties = parameters.GetType().GetProperties();
                var attributeClauses = new List<string>();

                for (int i = 0; i < properties.Length; i++)
                {
                    attributeClauses.Add($"{ToSnakeCase(properties[i].Name)} := @{properties[i].Name}");
                    p.Add(properties[i].Name, properties[i].GetValue(parameters));
                }

                storedProcedure += $"({string.Join(", ", attributeClauses)})";
            }

            try
            {
                var rows = await _unitOfWork.Connection.QueryAsync<U>(storedProcedure, p);
                return rows.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task SaveAsync<P>(string storedProcedure, P parameters)
        {
            PropertyInfo[] properties;
            var p = new DynamicParameters();

            if (parameters is not null)
            {
                properties = parameters.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    p.Add(ToSnakeCase(properties[i].Name), properties[i].GetValue(parameters));
                }
            }

            try
            {
                await _unitOfWork.Connection.ExecuteAsync(storedProcedure, p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<PropertyInfo> GetPropertyInfo()
        {
            return typeof(T)
                .GetProperties().Where(p => p.Name != "Id" && !p.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute)));
        }

        private static string ToSnakeCase(string input)
        {
            return string.Concat(input.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c.ToString() : c.ToString())).ToLower();
        }

        private PropertyInfo? GetPrimaryKeyInfo()
        {
            return typeof(T)
                .GetProperties().Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))).FirstOrDefault();
        }

        public string ConvertSql(string sql)
        {
            // Get the properties of the class
            var properties = typeof(T).GetProperties().Where(p => !p.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute)));

            // Build the list of columns
            var columns = new List<string>();
            foreach (var property in properties)
            {
                // Get the column name for the property
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                var columnName = columnAttribute != null ? columnAttribute.Name : ToSnakeCase(property.Name);

                // Add the column to the list, with an optional alias
                columns.Add(columnName == property.Name ? columnName : $"{columnName} as {property.Name}");
            }

            // Replace the SELECT * with the list of columns
            return sql.Replace("*", string.Join(", ", columns));
        }
    }
}


//public virtual async Task<ICollection<U>> CustomLoadAsync<U, P>(string sqlStatement, P parameters, bool isStoredProcedure = false)
//{
//    PropertyInfo[] properties;
//    var p = new DynamicParameters();
//    var attributeClauses = new List<string>();

//    if (parameters is not null)
//    {
//        properties = parameters.GetType().GetProperties();

//        for (int i = 0; i < properties.Length; i++)
//        {
//            attributeClauses.Add($"{ToSnakeCase(properties[i].Name)} := @{properties[i].Name}");
//            p.Add(properties[i].Name, properties[i].GetValue(parameters));
//        }
//    }

//    if (isStoredProcedure == true)
//    {
//        sqlStatement = $"SELECT {sqlStatement}";
//        sqlStatement += $"({string.Join(", ", attributeClauses)})";
//    }

//    try
//    {
//        var rows = await _unitOfWork.Connection.QueryAsync<U>(sqlStatement, p);
//        return rows.ToList();
//    }
//    catch (Exception)
//    {
//        throw;
//    }
//}

//public virtual async Task<int> CustomSaveAsync<P>(string sqlStatement, P parameters, bool isStoredProcedure = false)
//{
//    var p = new DynamicParameters();
//    CommandType commandType = CommandType.Text;
//    PropertyInfo[] properties;

//    if (parameters is not null)
//    {
//        properties = parameters.GetType().GetProperties();
//        for (int i = 0; i < properties.Length; i++)
//        {
//            p.Add(ToSnakeCase(properties[i].Name), properties[i].GetValue(parameters));
//        }
//    }

//    if (isStoredProcedure == true)
//    {
//        commandType = CommandType.StoredProcedure;
//    }

//    try
//    {
//        return await _unitOfWork.Connection.ExecuteAsync(sqlStatement, p, commandType: commandType);
//    }
//    catch (Exception)
//    {
//        throw;
//    }
//}