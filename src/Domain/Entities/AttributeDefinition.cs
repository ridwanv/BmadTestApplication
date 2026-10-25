using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities
{
    public class AttributeDefinition : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string DataType { get; set; } = default!;
        public bool Mandatory { get; set; }
        public List<string>? AllowedValues { get; set; }
        public string? Regex { get; set; }
    }
}
