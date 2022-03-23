namespace SiT.Scheduler.API.Extensions;

using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.Utilities.OperationResults;

public static class ControllerExtensions
{
    public static IActionResult Error(this ControllerBase controllerBase, IOperationResult operationResult) => controllerBase.BadRequest(operationResult.ExtractErrors());
}