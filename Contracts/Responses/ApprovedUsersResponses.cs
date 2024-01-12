namespace CashFlowAPI.Contracts.Responses;

public record APIOkResposne(string Message);
public record APIDataResposne : APIOkResposne
{
    public object? Data { get; init; }
    public APIDataResposne(string message, object? data) : base(message)
    {
        Message = message;
        Data = data;
    }
}

public record APIErrorResponse(string status, string message);
