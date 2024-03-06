using FluentValidation;

namespace TheSpine.Application.Features.Commands
{
    public class GenericValidator<T> : AbstractValidator<T> where T : class
    {
        public Func<object, string, Task<IEnumerable<string>>> ValidateModel => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
