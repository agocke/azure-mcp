// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Core.Areas.Subscription.Commands;

[JsonSerializable(typeof(SubscriptionListCommand.SubscriptionListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class SubscriptionJsonContext : JsonSerializerContext
{

}
