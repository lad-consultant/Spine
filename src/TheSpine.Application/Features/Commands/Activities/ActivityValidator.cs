using FluentValidation;
using TheSpine.Application.Features.Commands.Activities.Create;

namespace TheSpine.Application.Features.Commands.Activities
{
    public class ActivityValidator : AbstractValidator<CreateActivityCommand>
    {
        public ActivityValidator()
        {
            RuleFor(activity => activity.ViewModel)
                .SetValidator(new ActivityViewModelValidator());
        }
    }

    public class ActivityViewModelValidator : GenericValidator<ActivityViewModel>
    {
        public ActivityViewModelValidator()
        {
            RuleFor(activityVM => activityVM.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");

            RuleFor(activityVM => activityVM.Description)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull()
               .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");
        }
    }
}
