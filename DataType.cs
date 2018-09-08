using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DatabaseCommon
{
    public class DataType
    {
        public static Type GetColumnCSharpDataType(
            SqlDbType dataType
        )
        {
            switch (dataType)
            {
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                    return typeof(DateTime);
                case SqlDbType.BigInt:
                case SqlDbType.Int:
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt:
                    return typeof(Int32);
                default:
                    return typeof(String);
            }
        }

    }
}
