using Xunit;
using Itau.Api.Login.Models;
using FluentValidation.TestHelper;

namespace Tests.Units.Itau.Login
{
  [Collection("Global")]
  public class LoginTests : IDisposable
  {
    private readonly LoginValidator _validarSenhaValidator;

    public LoginTests()
    {
        _validarSenhaValidator = new LoginValidator();
    }

    [Fact(DisplayName = "Login - Senha deve ser válida")]
    [Trait("Categoria", "Login")]
    public async Task Login_Senha_DeveSerValido()
    {
      // Arrange
      var login = new LoginBuilder()
        .WithSenha("AbTp9!fok")
        .Build();

      // Act
      var result = await _validarSenhaValidator.TestValidateAsync(login);

      // Assert
      result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Login - Não deve ser null ou vazio")]
    [Trait("Categoria", "Login")]
    [InlineData("")]
    [InlineData(null)]
    public async Task Login_Senha_DeveRetornarInconsistencias(
      string senha
    )
    {
      // Arrange
      var login = new LoginBuilder()
        .WithSenha(senha)
        .Build();

      // Act
      var result = await _validarSenhaValidator.TestValidateAsync(login);

      // Assert
      result.ShouldHaveValidationErrorFor(x => x.Senha).WithErrorMessage("Não deve estar em branco");
    }

    [Theory(DisplayName = "Login - Validar senha com caracteres repetidos")]
    [Trait("Categoria", "Login")]
    [InlineData("!22")]
    [InlineData("Abcb")]
    [InlineData("AbTp9!foo")]
    [InlineData("AbTp9!foA")]
    public async Task Login_Senha_DeveValidarCaracteresDuplicados(string senha)
    {
      // Arrange
      var login = new LoginBuilder()
        .WithSenha(senha)
        .Build();

      // Act
      var result = await _validarSenhaValidator.TestValidateAsync(login);

      // Assert
      result.ShouldHaveValidationErrorFor(x => x.Senha).WithErrorMessage("Não é permitido ter caracteres repetidos.");
    }

    [Theory(DisplayName = "Login - Senha invalida")]
    [Trait("Categoria", "Login")]
    [InlineData("Aabcdefgh@")]  // Ausência do dígito
    [InlineData("aabcdefg1@")]  // Ausência da letra maiúscula
    [InlineData("ABCDEFG1@")]   // Ausência da letra minúscula
    [InlineData("Abcdefgh1")]   // Ausência de caractere especial
    [InlineData("AbTp9 fo!")]   // Senha com espaço
    [InlineData("AbCd7@qt")]    // Senha com menos de 9 caracteres
    public async Task Login_Senha_DeveValidarSenhaInvalida(string senha)
    {
      // Arrange
      var login = new LoginBuilder()
        .WithSenha(senha)
        .Build();

      // Act
      var result = await _validarSenhaValidator.TestValidateAsync(login);

      // Assert
      result.ShouldHaveValidationErrorFor(x => x.Senha).WithErrorMessage(
        "A senha deve ter no mínimo 9 caracteres, composta por no mínimo: " +
        "1 dígito. " +
        "1 letra minúscula. " +
        "1 letra maiúscula. " +
        "1 caractere especial. " +
        "Não é permitido utilizar espaços."
      );
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
  }
}
