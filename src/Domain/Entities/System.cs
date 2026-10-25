// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class System : BaseAuditableEntity, IMayHaveTenant
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string CotsCustom { get; set; } = string.Empty;
    public string HostingCurrent { get; set; } = string.Empty;
    public string HostingTarget { get; set; } = string.Empty;
    public string InsourceOutsource { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? TenantId { get; set; }
    
    // Navigation properties for hierarchical relationships
    public virtual ICollection<Subsystem> Subsystems { get; set; } = new List<Subsystem>();
}