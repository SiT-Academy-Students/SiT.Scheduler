namespace SiT.Scheduler.StorageManagement.Contracts;

using SiT.Scheduler.Utilities.OperationResults;

public interface IStorageManager
{
    Task<IOperationResult<IFileUploadResult>> UploadFileAsync(string fileName, Stream file, CancellationToken cancellationToken);
}