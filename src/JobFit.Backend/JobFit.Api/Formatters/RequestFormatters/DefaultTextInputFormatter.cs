using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace JobFit.Api.Formatters.RequestFormatters;

/// <summary>
/// Represents default text input formatter that just reads request body
/// </summary>
public class DefaultTextInputFormatter : InputFormatter
{
    private const string MimeType = "text/plain";

    public DefaultTextInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(MimeType));
    }

    public override bool CanRead(InputFormatterContext context) => context.HttpContext.Request.ContentType?.StartsWith(MimeType) ?? false;

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        var request = context.HttpContext.Request;
        using var reader = new StreamReader(request.Body);
        var content = await reader.ReadToEndAsync();
        return await InputFormatterResult.SuccessAsync(content);
    }
}