using Microsoft.AspNetCore.Mvc;
using Itau.Api.Login.Models;
using FluentValidation;
using Itau.Infrastructure.Common;

namespace Itau.Api.Login
{
  [Route("login")]
  [ApiController]
  public class LoginController : BaseController
  {
    private readonly ILoginService _validarSenhaService;
    private readonly IValidator<LoginDadosRequest> _validator;

    public LoginController(
      ILoginService validarSenhaService,
      IValidator<LoginDadosRequest> validator
    )
    {
      _validarSenhaService = validarSenhaService;
      _validator = validator;
    }

    #region Metodos Publicos

    /// <summary>
    /// Realiza a validação  de uma senha
    /// </summary>
    /// <param name="dados">Dados para validar senha</param>
    [HttpPost("validar-senha")]
    public async Task<ActionResult<Models.Login>> Post([FromBody] LoginDadosRequest dados)
      => CustomResponse(await _validarSenhaService.ValidarSenha(dados));

    #endregion Metodos Publicos
  }
}
