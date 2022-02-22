namespace SiT.Scheduler.StorageManagement;

using SiT.Scheduler.StorageManagement.Contracts;
using SiT.Scheduler.Utilities.OperationResults;

public class FallbackStorageManager : IStorageManager
{
    public Task<IOperationResult<IFileUploadResult>> UploadFileAsync(Stream file, CancellationToken cancellationToken) => Task.FromResult<IOperationResult<IFileUploadResult>>(new OperationResult<IFileUploadResult>());
}