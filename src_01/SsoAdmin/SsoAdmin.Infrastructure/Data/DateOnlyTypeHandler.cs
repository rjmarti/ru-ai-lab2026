using System.Data;
using Dapper;

namespace SsoAdmin.Infrastructure.Data;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value) =>
        DateOnly.Parse((string)value);

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.ToString("yyyy-MM-dd");
    }
}
