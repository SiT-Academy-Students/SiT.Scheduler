namespace SiT.Scheduler.StorageManagement.CloudinaryImpl;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using SiT.Scheduler.StorageManagement.Contracts;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;
using CloudinaryConfiguration = SiT.Scheduler.StorageManagement.CloudinaryImpl.Configuration.CloudinaryConfiguration;

public class CloudinaryStorageManager : IStorageManager
{
    private readonly Cloudinary _cloudinary;
    private readonly string _assetFolder;

    public CloudinaryStorageManager(IOptions<CloudinaryConfiguration> cloudinaryConfiguration)
    {
        var config = cloudinaryConfiguration.Value ?? throw new ArgumentNullException(nameof(cloudinaryConfiguration));
        var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
        this._cloudinary = new Cloudinary(account) { Api = { Secure = true } };
        this._assetFolder = config.AssetFolder;
    }

    public async Task<IOperationResult<IFileUploadResult>> UploadFileAsync(Stream file, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IFileUploadResult>();
        operationResult.ValidateNotNull(file);
        if (!operationResult.IsSuccessful) return operationResult;

        var rawUploadParams = new RawUploadParams { File = new FileDescription("file_name", file), Folder = this.GetAssetFolder() };

        try
        {
            var uploadResult = await this._cloudinary.UploadAsync(rawUploadParams, cancellationToken: cancellationToken);
            if (uploadResult.Error != null) operationResult.AddError(new StandardError(uploadResult.Error.Message));
            else operationResult.Data = new FileUploadResult(uploadResult.SecureUrl.AbsoluteUri);
        }
        catch (Exception e)
        {
            var error = new ErrorFromException(e);
            operationResult.AddError(error);
        }

        return operationResult;
    }

    private string GetAssetFolder()
    {
        if (string.IsNullOrWhiteSpace(this._assetFolder)) return "sit/scheduler";
        return this._assetFolder;
    }
}