namespace Carts.Application.Common.Responses;

public class ApplicationErrorResponse
{
    public ApplicationErrorResponse(string code, string title, string description)
    {
        Code = code;
        Title = title;
        Description = description;
    }

    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
