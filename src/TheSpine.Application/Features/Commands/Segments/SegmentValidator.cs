using FluentValidation;
using TheSpine.Application.Features.Commands.Segments.Manage;

namespace TheSpine.Application.Features.Commands.Segments
{
    public class SegmentValidator : AbstractValidator<ManageSegmentCommand>
    {
        public SegmentValidator()
        {
            RuleFor(s => s.SegmentViewModel)
                .SetValidator(new SegmentViewModelValidator());
        }
    }

    public class SegmentViewModelValidator : GenericValidator<SegmentViewModel>
    {
        public SegmentViewModelValidator()
        {
            RuleFor(svm => svm.Title)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");
        }
    }
}
