namespace CashFlowAPI.DB;

public record MongoConfig
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}