using FluentValidation;

using TheSpine.Application.Features.Commands.Nodes.Manage;

namespace TheSpine.Application.Features.Commands.Nodes
{
    public class NodeValidator : AbstractValidator<ManageNodeCommand>
    {
        public NodeValidator()
        {
            RuleFor(n => n.Model)
                .SetValidator(new NodeViewModelValidator());
        }
    }

    public class NodeViewModelValidator : GenericValidator<NodeViewModel>
    {
        public NodeViewModelValidator()
        {
            RuleFor(nvm => nvm.Title)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");
        }
    }
}
