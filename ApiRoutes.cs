namespace CashFlowAPI;

public static class ApiRoutes
{
    private const string ApiBase = "api";

    public static class Infra
    {
        public const string IsAlive = "/";
    }

    public static class Account
    {
        private const string Base = $"{ApiBase}/account";
        public const string GetTransactions = $"{Base}/transactions";
        public const string GetTransaction = $"{Base}/transactions/{{id:guid}}";
        public const string CreateTransaction = $"{Base}/transactions";
        public const string DeleteTransaction = $"{Base}/transactions/{{id:guid}}";
    }
}