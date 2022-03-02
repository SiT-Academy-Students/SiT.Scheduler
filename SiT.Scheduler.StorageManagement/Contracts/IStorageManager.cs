namespace SiT.Scheduler.StorageManagement.Contracts;

using SiT.Scheduler.Utilities.OperationResults;

public interface IStorageManager
{
    Task<IOperationResult<IFileUploadResult>> UploadFileAsync(Stream file, CancellationToken cancellationToken);
}