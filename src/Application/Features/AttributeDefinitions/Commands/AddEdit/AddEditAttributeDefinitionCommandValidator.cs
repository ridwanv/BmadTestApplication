using FluentValidation;

namespace CleanArchitecture.Blazor.Application.Features.AttributeDefinitions.Commands.AddEdit
{
    public class AddEditAttributeDefinitionCommandValidator : AbstractValidator<AddEditAttributeDefinitionCommand>
    {
        public AddEditAttributeDefinitionCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DataType).NotEmpty();
            RuleFor(x => x.Regex).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Regex));
        }
    }
}
