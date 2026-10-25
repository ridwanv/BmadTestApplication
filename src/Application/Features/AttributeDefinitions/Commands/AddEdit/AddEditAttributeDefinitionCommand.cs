using System.Collections.Generic;
using MediatR;

namespace CleanArchitecture.Blazor.Application.Features.AttributeDefinitions.Commands.AddEdit
{
    public class AddEditAttributeDefinitionCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string Name { get; set; } = default!;
        public string DataType { get; set; } = default!;
        public bool Mandatory { get; set; }
        public List<string>? AllowedValues { get; set; }
        public string? Regex { get; set; }
    }
}
