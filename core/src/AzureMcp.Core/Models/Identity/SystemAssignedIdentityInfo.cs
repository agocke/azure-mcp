// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Core.Models.Identity;

public class SystemAssignedIdentityInfo
{
    public bool Enabled { get; set; }
    public string? TenantId { get; set; }
    public string? PrincipalId { get; set; }
}
