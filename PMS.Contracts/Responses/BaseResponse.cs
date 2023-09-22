namespace PMS.Contracts.Responses;

public class BaseResponse
{
    public bool Success { get; set; } = true;
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? ValidationErrors { get; set; }
    public Object? Data { get; set; }
}