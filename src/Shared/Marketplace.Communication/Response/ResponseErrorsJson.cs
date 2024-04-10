namespace Marketplace.Communication.Response;

public class ResponseErrorsJson
{
    public List<string> Messages { get; set; }

    public ResponseErrorsJson(string message)
    {
        Messages = new List<string> { message };
    }

    public ResponseErrorsJson(List<string> messages)
    {
        Messages = messages;
    }
}
