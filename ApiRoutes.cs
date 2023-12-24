namespace CashFlowAPI;

public static class ApiRoutes
{
    private const string ApiBase = "api";

    public static class Infra
    {
        public const string IsAlive = "/";
    }

    public static class Transactions
    {
        private const string Base = $"{ApiBase}/{{aid:guid}}/transactions";
        public const string GetTransactions = Base;
        public const string GetTransaction = $"{Base}/{{id:guid}}";
        public const string CreateTransaction = Base;
        public const string DeleteTransaction = $"{Base}/{{id:guid}}";
    }

    public static class ApprovedUsers
    {
        private const string Base = $"{ApiBase}/approved-users";
        public const string ApproveUser = Base;
        public const string DisapproveUser = $"{Base}/{{guidOrUsername}}";
        public const string GetApprovedUsers = Base;
        public const string GetApprovedUser = $"{Base}/{{guidOrUsername}}";
    }
}