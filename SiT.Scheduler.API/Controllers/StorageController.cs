namespace SiT.Scheduler.API.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.StorageManagement.Contracts;

[ApiController]
[Route("storage")]
public class StorageController : ControllerBase
{
    [NotNull]
    private readonly IStorageManager _storageManager;

    public StorageController([NotNull] IStorageManager storageManager)
    {
        this._storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFromRequestBodyAsync(CancellationToken cancellationToken)
    {
        var fileContent = this.Request.Body;
        var uploadFile = await this._storageManager.UploadFileAsync(fileContent, cancellationToken);
        if (!uploadFile.IsSuccessful) return this.Error(uploadFile);

        return this.Ok(uploadFile.Data.Url);
    }

    [HttpPost("form-upload")]
    public async Task<IActionResult> UploadFromFormAsync([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null) return this.BadRequest("Provide a valid file.");

        await using var fileContent = file.OpenReadStream();
        var uploadFile = await this._storageManager.UploadFileAsync(fileContent, cancellationToken);
        if (!uploadFile.IsSuccessful) return this.Error(uploadFile);

        return this.Ok(uploadFile.Data.Url);
    }
}