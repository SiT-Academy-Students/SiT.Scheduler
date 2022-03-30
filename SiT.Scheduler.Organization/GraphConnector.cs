namespace SiT.Scheduler.Organization;

using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using SiT.Scheduler.Organization.Configuration;
using SiT.Scheduler.Organization.Contracts;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;

public class GraphConnector : IGraphConnector
{
    private readonly GraphServiceClient _graphServiceClient;

    public GraphConnector(IOptions<GraphConnectorConfiguration> configurationOptions)
    {
        var configuration = configurationOptions?.Value ?? throw new ArgumentNullException(nameof(configurationOptions));

        var clientSecretCredential = new ClientSecretCredential(configuration.TenantId, configuration.ClientId, configuration.ClientSecret);
        this._graphServiceClient = new GraphServiceClient(clientSecretCredential);
    }

    public async Task<IOperationResult<IExternalIdentity>> GetExternalIdentityAsync(Guid userId, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IExternalIdentity>();
        operationResult.ValidateNotDefault(userId);
        if (!operationResult.IsSuccessful) return operationResult;

        try
        {
            var user = await this._graphServiceClient.Users[userId.ToString()].Request().Select(x => new { x.UserPrincipalName, x.DisplayName, x.Id }).GetAsync(cancellationToken);
            operationResult.Data = new ExternalIdentity(userId, user.DisplayName, user.UserPrincipalName);
        }
        catch (Exception e)
        {
            operationResult.AddError(new ErrorFromException(e));
        }

        return operationResult;
    }
}