using FluentValidation;
using TheSpine.Application.Features.Commands.ItemDetailedInfo.Manage;

namespace TheSpine.Application.Features.Commands.ItemDetailedInfo
{
	public class ItemDetailedInfoValidator : AbstractValidator<ManageItemDetailedInfoCommand>
	{
		public ItemDetailedInfoValidator()
		{
			RuleFor(info => info.ViewModel)
				.SetValidator(new ItemDetailedInfoViewModelValidator());
		}
	}

	public class ItemDetailedInfoViewModelValidator : GenericValidator<ItemDetailedInfoViewModel>
	{
		public ItemDetailedInfoViewModelValidator()
		{
			RuleFor(infovm => infovm.Title)
				.NotEmpty().WithMessage("{PropertyName} is required")
				.NotNull()
				.MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");

			RuleFor(infovm => infovm.HtmlContent)
			   .NotEmpty().WithMessage("{PropertyName} is required")
			   .NotNull()
			   .MaximumLength(2000).WithMessage("{PropertyName} must not exceed 2000 characters");
		}
	}
}
