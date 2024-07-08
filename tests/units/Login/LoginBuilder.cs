using Itau.Api.Login.Models;

namespace Tests.Units.Itau.Login
{
  public class LoginBuilder : LoginDadosRequest
  {
    public LoginDadosRequest Build()
    {
      return new LoginDadosRequest
      {
        Senha = Senha
      };
    }

    public LoginBuilder WithSenha(string senha)
    {
      Senha = senha;
      return this;
    }
  }
}
