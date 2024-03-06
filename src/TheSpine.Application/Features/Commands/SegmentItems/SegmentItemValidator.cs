using FluentValidation;
using TheSpine.Application.Features.Commands.SegmentItems.Manage;

namespace TheSpine.Application.Features.Commands.SegmentItems
{
    public class SegmentItemValidator : AbstractValidator<ManageSegmentItemCommand>
    {
        public SegmentItemValidator()
        {
            RuleFor(si => si.SegmentItemViewModel)
                .SetValidator(new SegmentItemViewModelValidator());
        }
    }

    public class SegmentItemViewModelValidator : GenericValidator<SegmentItemViewModel>
    {
        public SegmentItemViewModelValidator()
        {
            RuleFor(sivm => sivm.Title)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");

            RuleFor(sivm => sivm.Rating)
                .NotEmpty().WithMessage("{PropertyName} value should be in range of 1 to 5")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} value should be in range of 1 to 5")
                .LessThanOrEqualTo(5).WithMessage("{PropertyName} value should be in range of 1 to 5");

            RuleFor(sivm => sivm.Differentiator)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");

            RuleFor(sivm => sivm.Segment)
                .NotEmpty().WithMessage("Category is required")
                .NotNull();

            RuleFor(sivm => sivm.EaseOfUse)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(sivm => sivm.Licensing)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");

            RuleFor(sivm => sivm.Notes)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");
        }
    }
}
