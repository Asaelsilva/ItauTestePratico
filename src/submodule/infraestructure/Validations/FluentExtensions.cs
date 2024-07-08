using System.Text.RegularExpressions;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace Itau.Infrastructure.Validations.FluentExtensions
{
  public static partial class FluentExtensions
  {
    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()\-+\=])(?!.*\s).{9,}$")]
    private static partial Regex Senha();

    public static IRuleBuilderOptions<T, string> ValidarSenha<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
    {
        return ruleBuilder
          .Must(senha => Senha().IsMatch(senha));
    }

    public static IRuleBuilderOptions<T, string> NotEmptyOrNull<T>(
      this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.Must(value => !string.IsNullOrEmpty(value));
    
    public static bool ContainsAllKeys(this JObject jsonObject, params string[] properties)
    {
      foreach (string value in properties)
      {
        if (!string.IsNullOrEmpty(value) && !jsonObject.ContainsKey(value))
          return false;
      }
      return true;
    }
  }
}
