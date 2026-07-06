using Dapper;

namespace SsoAdmin.Infrastructure.Data;

public class SchemaInitializer
{
    private readonly DbConnectionFactory _factory;

    public SchemaInitializer(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public void Initialize()
    {
        using var conn = _factory.Create();
        conn.Execute(Schema.Sql);
    }
}
