using System.Text.Json;

namespace Drug_Procurement.Wrappers;

public class Response<T>
{
    public Response()
    {
         
    }
    public Response(T data, string message, string code)
    {
        Code = code;
        Data = data;
        Message = message;
        Succeeded = true;
    }
    public Response(T data, string message, string code, bool succeeded)
    {
        Code= code;
        Data = data;
        Message = message;
        Succeeded = succeeded;
    }
    public Response(T? data)
    {
        Data = data;
        Succeeded = true;
    }
    public Response(string? message)
    {
        Message = message;
        Succeeded = true;
    }

    public string? Message { get; set; }
    public bool Succeeded { get; set; }
    public T? Data { get; set; }
    public string? Code { get; set; }
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    public override string ToString() => JsonSerializer.Serialize(this);
}
