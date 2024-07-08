using FluentValidation;
using Itau.Infrastructure.Validations.FluentExtensions;

namespace Itau.Api.Login.Models
{
  public class LoginValidator : AbstractValidator<LoginDadosRequest>
  {
    public LoginValidator()
    {
      RuleFor(r => r.Senha)
        .NotEmptyOrNull()
          .WithMessage("Não deve estar em branco");

      When(x => !string.IsNullOrEmpty(x.Senha), () =>
      {
        RuleFor(r => r.Senha)
          .Must(x => !PossuiCaracteresRepetidos(x))
            .WithMessage("Não é permitido ter caracteres repetidos.")
          .ValidarSenha()
            .WithMessage("A senha deve ter no mínimo 9 caracteres, composta por no mínimo: " +
              "1 dígito. " +
              "1 letra minúscula. " +
              "1 letra maiúscula. " +
              "1 caractere especial. " +
              "Não é permitido utilizar espaços."
            );
      });
    }

    private static bool PossuiCaracteresRepetidos(string senha)
    {
      return senha?.Distinct().Count() != senha?.Length;
    }
  }
}
