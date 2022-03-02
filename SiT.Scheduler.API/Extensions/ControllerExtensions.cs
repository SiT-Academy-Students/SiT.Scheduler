namespace SiT.Scheduler.API.Extensions;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.Utilities;
using SiT.Scheduler.Utilities.OperationResults;

public static class ControllerExtensions
{
    public static IActionResult Error(this ControllerBase controllerBase, IOperationResult operationResult)
    {
        var sb = new StringBuilder();
        foreach (var error in (operationResult?.Errors).OrEmptyIfNull().IgnoreNullValues()) sb.AppendLine(error.ErrorMessage);

        return controllerBase.BadRequest(sb.ToString());
    }
}