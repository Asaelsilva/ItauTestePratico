
namespace Microsoft.Extensions.DependencyInjection
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
      app.UseMiddleware(typeof(Itau.Infrastructure.Middlewares.ErrorHandlingMiddleware));
      return app;
    }

    public static IApplicationBuilder UserCommonsMiddlewares(this WebApplication app)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();
      app.UseErrorHandlingMiddleware();

      return app;
    }
  }
}
