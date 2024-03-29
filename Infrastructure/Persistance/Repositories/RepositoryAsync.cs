﻿using Application.Contracts.Persistance;
using Application.Models.Persistance;
using Dapper;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Template.Infrastructure.Persistance.Dapper.Repositories;


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
                _tableName = typeof(T).Name;
            }

            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> SelectByIdAsync<Tid>(Tid id)
        {
            try
            {
                var sql = $"SELECT * FROM {_tableName} WHERE id = @id";
                var result = await _unitOfWork.Connection.QueryFirstAsync<T>(
                    sql,
                    new { id });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> SelectAllAsync()
        {
            try
            {
                var sql = ConvertSql($"SELECT * FROM {_tableName}");
                return await _unitOfWork.Connection.QueryAsync<T>(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> SelectByParameterAsync(QueryParameter queryParameter)
        {
            // Build the SQL query
            var sql = ConvertSql($"SELECT * FROM {_tableName}");
            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();
            if (queryParameter.Condition.Count > 0)
            {
                // Add WHERE clauses for each filter condition
                var i = 0;
                foreach (var condition in queryParameter.Condition)
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
            if (queryParameter.OrderByColumn.Count > 0)
            {
                // Add ORDER BY clause for each OrderByColumn
                var orderByClauses = queryParameter.OrderByColumn.Select(x => $"{x}");
                sql += $" ORDER BY {string.Join(", ", orderByClauses)}";
            }
            if (queryParameter.Limit.HasValue)
            {
                sql += $" LIMIT {queryParameter.Limit}";
                // Add LIMIT and OFFSET clauses for paging
                if (queryParameter.Offset.HasValue)
                {
                    sql += $" OFFSET {queryParameter.Offset}";
                }
            }

            // Execute the query and return the results
            return await _unitOfWork.Connection.QueryAsync<T>(sql, parameters);
        }

        public virtual async Task<T> AddAsync(T entity)
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
                var properties = GetPropertyInfo();
                var primaryKeyClauses = string.Join(" AND ", properties.Select(c => $"{ToSnakeCase(c.Name)} = @{c.Name}"));
                var updates = string.Join(',', GetPropertyInfo().Where(p => !p.CustomAttributes
                                                                .Any(a => a.AttributeType == typeof(KeyAttribute)))
                                                                .Select(c => $"{ToSnakeCase(c.Name)} = @{c.Name}"));

                var sql = $"UPDATE {_tableName} SET {updates} WHERE id = @id";
                if (!string.IsNullOrEmpty(primaryKeyClauses))
                {
                    sql = $"UPDATE {_tableName} SET {updates} WHERE {primaryKeyClauses}";
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
                var primaryKeyClauses = string.Join(" AND ", GetPrimaryKeyInfo().Select(c => $"{ToSnakeCase(c.Name)} = @{c.Name}"));

                var sql = $"DELETE FROM {_tableName} WHERE id = @id";

                if (!string.IsNullOrEmpty(primaryKeyClauses))
                {
                    sql = $"DELETE FROM {_tableName} WHERE {primaryKeyClauses}";
                }

                return await _unitOfWork.Connection.ExecuteAsync(sql, entity) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<U>> LoadAsync<U, P>(string storedProcedure, P parameters)
        {
            FieldInfo[] fields = typeof(U).GetFields();
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
                return await _unitOfWork.Connection.QueryAsync<U>(storedProcedure, p);
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
            return typeof(T).GetProperties().Where(p => p.Name != "Id" && p.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute)));
        }

        private static string ToSnakeCase(string input)
        {
            return string.Concat(input.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c.ToString() : c.ToString())).ToLower();
        }

        private IEnumerable<PropertyInfo> GetPrimaryKeyInfo()
        {
            return typeof(T).GetProperties().Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)));
        }

        public string ConvertSql(string sql)
        {
            // Get the properties of the class
            var properties = typeof(T).GetProperties().Where(p => !p.GetMethod.IsVirtual);

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

//var sql = @"select * from trade; 
//                            select * from strategy where id = @id;
//                            select * from account where id = @id;";
//var reader = await _unitOfWork.Connection.QueryMultipleAsync(sql, param: new { id });
//var trade = (await reader.ReadAsync<Trade>()).Single();
//trade.Strategy = (await reader.ReadAsync<TradeStrategy>()).Single();
//trade.Account = (await reader.ReadAsync<Account>()).Single();

//try
//{
//    var result = await _unitOfWork.Connection.QueryAsync<Trade, Account, Trade>(
//    storedProcedure,
//    param: p,
//    map: (u, c) => {
//        u.Account = c;
//        return u;
//    },
//    splitOn: "id"
//    );
//    return null;
//}
//catch (Exception)
//{
//    throw;
//}