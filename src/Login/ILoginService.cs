
using Itau.Api.Login.Models;

namespace Itau.Api.Login
{
  public interface ILoginService
  {
    Task<Models.Login> ValidarSenha(LoginDadosRequest dados);
  }
}
