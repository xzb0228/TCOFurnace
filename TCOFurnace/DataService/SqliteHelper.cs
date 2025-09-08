using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using TCOFurnace.Common;

namespace TCOFurnace
{
    public static class SqliteHelper
    {
        internal static readonly string DataSourc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tcofurnace.db");
        private static readonly string connStr = $"Data Source={DataSourc};Version=3;";
        public static SQLiteConnection GetConn() => new SQLiteConnection(connStr);

        // 执行 SQL 命令（插入、更新、删除）
        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters=null)
        {
            using (var connection = new SQLiteConnection(connStr))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        // 执行查询并返回第一行第一列
        public static object ExecuteScalar(string sql, SQLiteParameter[] parameters=null)
        {
            using (var connection = new SQLiteConnection(connStr))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteScalar();
                }
            }
        }

        // 执行查询并返回 DataReader
        public static SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] parameters)
        {
            var connection = new SQLiteConnection(connStr);
            connection.Open();
            var command = new SQLiteCommand(sql, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            // 使用 CommandBehavior.CloseConnection 确保 reader 关闭时连接也关闭
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        // 执行查询并返回 DataTable
        public static DataTable GetDataTable(string sql, SQLiteParameter[] parameters=null)
        {
            using (var connection = new SQLiteConnection(connStr))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public static IEnumerable<T> Query<T>(string sql, SQLiteParameter[] parameters = null) where T : new()
        {
            DataTable dataTable = GetDataTable(sql, parameters);

            List<T> list = new List<T>();
            if (dataTable == null || dataTable.Rows.Count == 0)
                return list;

            var columnDict = dataTable.Columns.Cast<DataColumn>()
                .ToDictionary(
                    col => col.ColumnName.ToLowerInvariant(), // 键：小写列名
                    col => col,                              // 值：实际列
                    StringComparer.OrdinalIgnoreCase         // 字典键忽略大小写（可选）
                );
            // 获取业务类的所有属性
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (DataRow row in dataTable.Rows)
            {
                T entity = new T();
                foreach (PropertyInfo property in properties)
                {
                    // 忽略大小写查找匹配的列
                    string key = property.Name.ToLowerInvariant();
                    if (!columnDict.TryGetValue(key, out DataColumn column))
                        continue; // 列不存在则跳过

                    // 处理 DBNull
                    if (row.IsNull(column))
                        continue; // 可为 null 的属性可设置为 null，此处简化处理
                    try
                    {
                        // 转换列值为属性类型并赋值
                        object value = Convert.ChangeType(row[column], property.PropertyType);
                        property.SetValue(entity, value);
                    }
                    catch (Exception ex)
                    {
                        // 处理类型转换失败（如日期格式错误）
                        Log.Error($"表{typeof(T).Name}转换为实体失败：属性 {property.Name}，值 {row[column]}，错误：{ex.Message}");
                    }
                }
                list.Add(entity);
            }

            return list;
        }

        public static T QueryFirstOrDefault<T>(string sql, SQLiteParameter[] parameters = null) where T : new()
        {
            DataTable dataTable = GetDataTable(sql, parameters);

            if (dataTable == null || dataTable.Rows.Count == 0)
                return default(T);

            T entity = new T();
            // 获取业务类的所有属性
            PropertyInfo[] properties = typeof(T).GetProperties();
            DataRow row = dataTable.Rows[0];

            var columnDict = dataTable.Columns.Cast<DataColumn>()
              .ToDictionary(
                  col => col.ColumnName.ToLowerInvariant(), // 键：小写列名
                  col => col,                              // 值：实际列
                  StringComparer.OrdinalIgnoreCase         // 字典键忽略大小写（可选）
              );
            foreach (PropertyInfo property in properties)
            {
                // 忽略大小写查找匹配的列
                string key = property.Name.ToLowerInvariant();
                if (!columnDict.TryGetValue(key, out DataColumn column))
                    continue; // 列不存在则跳过
                if (column == null)
                    continue; // 列不存在则跳过

                // 处理 DBNull
                if (row.IsNull(column))
                    continue; // 可为 null 的属性可设置为 null，此处简化处理
                try
                {
                    // 转换列值为属性类型并赋值
                    object value = Convert.ChangeType(row[column], property.PropertyType);
                    property.SetValue(entity, value);
                }
                catch (Exception ex)
                {
                    // 处理类型转换失败（如日期格式错误）
                    Log.Error($"表{typeof(T).Name}转换为实体失败：属性 {property.Name}，值 {row[column]}，错误：{ex.Message}");
                }
            }
            return entity;
        }

        //获取列不区分大小写
        public static DataColumn GetColumnIgnoreCase(DataTable dataTable, string columnName)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                // 忽略大小写比较列名
                if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return column;
                }
            }
            return null; // 未找到匹配列
        }
    }
}
