using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Blazor.Application.Common.Interfaces;

namespace CleanArchitecture.Blazor.Application.Features.AttributeDefinitions.Commands.AddEdit
{
    public class AddEditAttributeDefinitionCommandHandler : IRequestHandler<AddEditAttributeDefinitionCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public AddEditAttributeDefinitionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(AddEditAttributeDefinitionCommand request, CancellationToken cancellationToken)
        {
            AttributeDefinition entity;
            if (request.Id.HasValue)
            {
                entity = await _context.AttributeDefinitions.FindAsync(new object[] { request.Id.Value }, cancellationToken);
                if (entity == null) return 0;
            }
            else
            {
                entity = new AttributeDefinition();
                _context.AttributeDefinitions.Add(entity);
            }
            entity.Name = request.Name;
            entity.DataType = request.DataType;
            entity.Mandatory = request.Mandatory;
            entity.AllowedValues = request.AllowedValues;
            entity.Regex = request.Regex;
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
