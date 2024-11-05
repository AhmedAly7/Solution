using Solution.Core.Infrastructures;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Solution.Data.Factories;

public class ParameterFactory : IParameterFactory
{
	public DbParameter GetParameter(string parameterName, object parameterValue, ParameterDirection direction = ParameterDirection.Input)
	{
		if (string.IsNullOrWhiteSpace(parameterName)) throw new ArgumentException(parameterName);
		if (parameterValue == null) parameterValue = DBNull.Value;

		if (!parameterName.StartsWith("@"))
			parameterName = string.Format("@{0}", parameterName);

		SqlParameter parameter = new SqlParameter(parameterName, parameterValue)
		{
			Direction = direction
		};

		if (parameter.SqlDbType == SqlDbType.Structured && parameterValue is DataTable)
			parameter.TypeName = (parameterValue as DataTable).TableName;

		return parameter;
	}

	public DbParameter GetParameter(string parameterName, object parameterValue, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
	{
		SqlParameter parameter = GetParameter(parameterName, parameterValue) as SqlParameter;
		parameter.Direction = direction;
		parameter.SqlDbType = (SqlDbType)dbType;

		if (parameter.SqlDbType == SqlDbType.Structured && parameterValue is DataTable)
			parameter.TypeName = (parameterValue as DataTable).TableName;

		return parameter;
	}

	public DbParameter GetParameter(string parameterName, object parameterValue, int size, ParameterDirection direction = ParameterDirection.Input)
	{
		SqlParameter parameter = GetParameter(parameterName, parameterValue) as SqlParameter;
		parameter.Size = size;
		parameter.Direction = direction;
		if (parameter.SqlDbType == SqlDbType.Structured && parameterValue is DataTable)
			parameter.TypeName = (parameterValue as DataTable).TableName;

		return parameter;
	}

	public DbParameter GetParameter(string parameterName, object parameterValue, DbType dbType, int size, ParameterDirection direction = ParameterDirection.Input)
	{
		SqlParameter parameter = GetParameter(parameterName, parameterValue) as SqlParameter;
		parameter.SqlDbType = (SqlDbType)dbType;
		parameter.Size = size;
		parameter.Direction = direction;

		if (parameter.SqlDbType == SqlDbType.Structured && parameterValue is DataTable)
			parameter.TypeName = (parameterValue as DataTable).TableName;

		return parameter;
	}
}