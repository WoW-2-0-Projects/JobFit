using JobFit.Application.Common.FileStorage.Brokers;
using JobFit.Application.Common.FileStorage.Services;
using JobFit.Domain.Entities;

namespace JobFit.Infrastructure.Common.FileStorage.Services;

/// <summary>
/// Provides storage file processing service functionality.
/// </summary>
public class StorageFileProcessingService(
    IFileContentTypeProvider fileContentTypeProvider,
    IFileStorageBroker fileStorageBroker,
    IStorageFileService storageFileService)
    : IStorageFileProcessingService
{
    public async ValueTask<StorageFile> CreateAsync(
        Stream stream,
        string fileName,
        string contentType,
        Guid ownerId,
        CancellationToken cancellationToken = default)
    {
        // Create storage file
        var extension = fileContentTypeProvider.GetExtension(contentType, fileName);
        var file = new StorageFile
        {
            OriginalName = fileName,
            PhysicalName = Path.ChangeExtension(Guid.NewGuid().ToString(), extension),
            ContentType = contentType,
            Extension = extension,
            OwnerId = ownerId,
            Size = (ulong)stream.Length
        };

        // Create file in database
        await storageFileService.CreateAsync(file, cancellationToken: cancellationToken);

        // Upload file content
        await fileStorageBroker.UploadAsync(file, stream);

        return file;
    }
}