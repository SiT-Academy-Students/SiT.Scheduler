namespace SiT.Scheduler.StorageManagement;

using SiT.Scheduler.StorageManagement.Contracts;

public record FileUploadResult(string Url) : IFileUploadResult;