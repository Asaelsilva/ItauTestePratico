using FluentValidation.Results;
using Itau.Infrastructure.Exceptions;

namespace FluentValidation
{
  public static class ValidatorExtensions
  {
    public static async Task<ValidationResult> ValidateAndThrowBusinessExceptionAsync<T>(
      this IValidator<T> validator,
      T instance,
      CancellationToken cancellationToken = default
    )
    {
      ValidationResult validation = await validator.ValidateAsync(instance, cancellationToken);
      if (!validation.IsValid)
        throw new BusinessException(validation);
      return validation;
    }
  }
}
