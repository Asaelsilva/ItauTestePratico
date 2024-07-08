namespace Itau.Infrastructure.Exceptions
{
  public class BusinessException : Exception
  {
    public FluentValidation.Results.ValidationResult FluentValidationResult { get; }

    public BusinessException() { }
    public BusinessException(string message) : base(message) { }
    public BusinessException(string message, Exception inner) : base(message, inner) { }
    public BusinessException(FluentValidation.Results.ValidationResult validationResult)
    {
      FluentValidationResult = validationResult;
    }
  }
}
