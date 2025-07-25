// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.AzureBestPractices.Commands;

[JsonSerializable(typeof(List<string>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class AzureBestPracticesJsonContext : JsonSerializerContext
{

}
