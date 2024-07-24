using JobFit.Application.Common.FileStorage.Brokers;
using JobFit.Domain.Entities;
using JobFit.Domain.Extensions;
using JobFit.Infrastructure.Common.FileStorage.Settings;
using JobFit.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;

namespace JobFit.Infrastructure.Common.FileStorage.Brokers;

/// <summary>
/// Provides the file storage service functionality.
/// </summary>
public class LocalFileStorageBroker(
    IOptions<LocalFileStorageSettings> localStorageSettings,
    IOptions<DomainSettings> domainSettings
)
    : IFileStorageBroker
{
    private readonly LocalFileStorageSettings _localFileStorageSettings = localStorageSettings.Value;
    private readonly DomainSettings _domainSettings = domainSettings.Value;


    public async ValueTask UploadAsync(StorageFile file, Stream? fileContentStream = default)
    {
        var fileRelativeUrl = GetFileRelativeUrl(file.PhysicalName);

        if (File.Exists(fileRelativeUrl)) return;

        var fileAbsolutePath = Path.Combine(_localFileStorageSettings.RootPath, fileRelativeUrl);

        var fileDirectoryPath = Path.GetDirectoryName(fileAbsolutePath) ??
                                throw new DirectoryNotFoundException("Uploading file is missing directory path");

        if (!Directory.Exists(fileDirectoryPath))
            Directory.CreateDirectory(fileDirectoryPath);

        // Write file content to the storage
        await using var fileStream = File.Create(fileAbsolutePath);

        if (fileContentStream != null) await fileContentStream.CopyToAsync(fileStream);
    }

    public ValueTask<string> GetFileAbsoluteUrlAsync(StorageFile storageFile)
    {
        return GetFileAbsoluteUrlAsync(storageFile.PhysicalName);
    }

    public ValueTask<string> GetFileAbsoluteUrlAsync(string fileName)
    {
        var relativeUrl = GetFileRelativeUrl(fileName);

        // TODO : change to file exception
        if (string.IsNullOrWhiteSpace(relativeUrl))
            throw new InvalidOperationException("File relative url is empty");

        var baseUrl = _domainSettings.ApiDomainUrl;

        var isValidRelativeUrl = Uri.IsWellFormedUriString(relativeUrl, UriKind.Relative);
        if (!isValidRelativeUrl)
            throw new InvalidOperationException($"Invalid url format : {relativeUrl}");

        var isValidBaseUrl = Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute);
        if (!isValidBaseUrl)
            throw new InvalidOperationException($"Invalid url format : {baseUrl}");

        // TODO : Fix url merging
        var absoluteUrl = new Uri(new Uri(baseUrl), relativeUrl);

        return ValueTask.FromResult($"{baseUrl}/{relativeUrl}");
    }


    public ValueTask DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);

        return ValueTask.CompletedTask;
    }

    public async ValueTask<Stream> GetFileContentAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found at path: {filePath}");

        // Read file content
        await using var fileStream = File.OpenRead(filePath);
        var memoryStream = new MemoryStream();

        // Copy file content to memory stream
        await fileStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }

    public string GetFileRelativeUrl(string fileName)
    {
        return Path.Combine("Employee", fileName).ToUrl();
    }
}