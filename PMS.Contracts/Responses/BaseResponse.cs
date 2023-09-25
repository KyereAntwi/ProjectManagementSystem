namespace PMS.Contracts.Responses;

public class BaseResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> ValidationErrors { get; set; } = new List<string>();
    public Object? Data { get; set; }
}