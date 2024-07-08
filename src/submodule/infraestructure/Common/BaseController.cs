using Microsoft.AspNetCore.Mvc;

namespace Itau.Infrastructure.Common
{
  public class BaseController : ControllerBase
  {
    /// <summary>
    /// Estrutura padrão de retorno para a API
    /// </summary>
    /// <typeparam name="T">Tipo do retorno</typeparam>
    public class Return<T>
    {
      public string Message { get; set; }
      public T Content { get; set; }
      public IDictionary<string, string[]> Errors { get; set; }
      public Exception Exception { get; set; }
    }

    /// <summary>
    /// Faz o tratamento padrão de retorno
    /// </summary>
    /// <typeparam name="T">Tipo do retorno</typeparam>
    /// <param name="data">Objeto que será retornado</param>
    /// <returns>OkObjectResult ou NoContentResult</returns>
    protected ActionResult<T> CustomResponse<T>(T data)
    {
      if (data == null) return new NoContentResult();

      var retorno = new Return<T>() { Content = data };

      if (data is string)
      {
        retorno.Content = default;
        retorno.Message = data.ToString();
      }
      return Ok(retorno);
    }
  }
}
