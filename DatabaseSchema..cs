using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseCommon
{
    public static class DatabaseSchema
    {
        public static bool IsNumericDataType(Column column)
        {
            string dataType = column.DataType.Name;
            if (dataType == "int" || dataType == "decimal" || dataType == "smallint" ||
                dataType == "tinyint" || dataType == "numeric")
            {
                return true;
            }
            return false;
        }

        public static bool HasIdentityColumn(
            Table table
        )
        {
            List<Column> columnList = new List<Column>();
            foreach (Column column in table.Columns)
            {
                if (column.Identity)
                {
                    return true;
                }
            }
            return false;
        }

        public static Database GetDatabase(
            string serverName,
            string databaseName
        )
        {
            Server server = new Server(serverName);
            if (server == null)
            {
                throw new ApplicationException("Server not found");
            }
            var database = server.Databases[databaseName];
            if (database == null)
            {
                throw new ApplicationException("Database not found");
            }
            return database;
        }

        public static Table GetTable(
            string serverName,
            string databaseName,
            string schemaName,
            string tableName
        )
        {
            var database = GetDatabase(serverName, databaseName);
            return GetTable(database, schemaName, tableName);
        }

        public static Table GetTable(
            Database database,
            string schemaName,
            string tableName
        )
        {
            var table = database.Tables[tableName, schemaName];
            if (table == null)
            {
                throw new ApplicationException("Table not found");
            }
            return table;
        }

        public static string GetColumnDataType(
            Column column
        )
        {
            string returnType = "";
            switch (column.DataType.Name)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    string length = column.DataType.MaximumLength.ToString();
                    if (length == "-1")
                    {
                        length = "max";
                    }
                    returnType = column.DataType.Name + "(" + length + ")";
                    break;
                case "decimal":
                case "numeric":
                    returnType = column.DataType.Name + "(" + column.DataType.NumericPrecision.ToString() + "," +
                        column.DataType.NumericScale + ")";
                    break;
                default:
                    returnType = column.DataType.Name;
                    break;
            }
            return returnType;
        }

    }
}
