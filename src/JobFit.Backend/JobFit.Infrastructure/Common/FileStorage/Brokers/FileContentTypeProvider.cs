using JobFit.Application.Common.FileStorage.Brokers;
using Microsoft.AspNetCore.StaticFiles;

namespace JobFit.Infrastructure.Common.FileStorage.Brokers;

/// <summary>
/// Provides the functionality for providing file content types.
/// </summary>
public class FileContentTypeProvider : IFileContentTypeProvider
{
    private readonly List<KeyValuePair<string, string>> _mappings;

    public FileContentTypeProvider()
    {
        var contentTypeProvider = new FileExtensionContentTypeProvider();

        // Initialize built-in provider values
        _mappings = contentTypeProvider.Mappings
            .Select(x => new KeyValuePair<string, string>(x.Key.StartsWith('.') ? x.Key.Substring(1) : x.Key, x.Value))
            .ToList();

        // Add missing extensions
        _mappings.Add(new("zip", "application/zip"));
        
        // Remove invalid extensions
        var invalidExtensions = new List<string>
        {
            "jpe"
        };

        _mappings.RemoveAll(mapping => invalidExtensions.Contains(mapping.Key));
    }

    public string GetExtension(string contentType, string? fileName = null)
    {
        var mapping = _mappings.Find(mapping => mapping.Value == contentType);
        return mapping.Key ?? string.Empty;
    }

    public bool IsValidContentType(string contentType)
    {
        return _mappings.Exists(mapping => mapping.Value == contentType);
    }

    public bool IsValidExtension(string extension)
    {
        return _mappings.Exists(mapping => mapping.Key == extension);
    }

    public bool IsMatchingContentTypeAndExtension(string contentType, string extension)
    {
        return _mappings.Exists(mapping => mapping.Key == extension && mapping.Value == contentType);
    }
}