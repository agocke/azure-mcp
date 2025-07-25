// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AppConfig.Commands;
using AzureMcp.AppConfig.Options.KeyValue;
using AzureMcp.AppConfig.Services;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.AppConfig.Commands.KeyValue;

public sealed class KeyValueDeleteCommand(ILogger<KeyValueDeleteCommand> logger) : BaseKeyValueCommand<KeyValueDeleteOptions>()
{
    private const string CommandTitle = "Delete App Configuration Key-Value Setting";
    private readonly ILogger<KeyValueDeleteCommand> _logger = logger;

    public override string Name => "delete";

    public override string Description =>
        """
        Delete a key-value pair from an App Configuration store. This command removes the specified key-value pair from the store.
        If a label is specified, only the labeled version is deleted. If no label is specified, the key-value with the matching
        key and the default label will be deleted.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = true, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var appConfigService = context.GetService<IAppConfigService>();
            await appConfigService.DeleteKeyValue(
                options.Account!,
                options.Key!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy,
                options.Label);

            var result = new KeyValueDeleteCommandResult(options.Key, options.Label);
            context.Response.Results = ResponseResult.Create(result, AppConfigJsonContext.Default.KeyValueDeleteCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred deleting value. Key: {Key}.", options.Key);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record KeyValueDeleteCommandResult(string? Key, string? Label);
}
