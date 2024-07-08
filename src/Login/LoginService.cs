using FluentValidation;
using Itau.Api.Login.Models;

namespace Itau.Api.Login
{
  public class LoginService : ILoginService
  {
    private readonly IValidator<LoginDadosRequest> _validator;
    
    public LoginService(IValidator<LoginDadosRequest> validator)
    {
      _validator = validator;
    }

    #region Metodos Publicos

    public async Task<Models.Login> ValidarSenha(LoginDadosRequest dados)
    {
      await _validator.ValidateAndThrowBusinessExceptionAsync(dados);

      return new()
      {
        SenhaValida = true
      };
    }

    #endregion Metodos Publicos
  }
}
