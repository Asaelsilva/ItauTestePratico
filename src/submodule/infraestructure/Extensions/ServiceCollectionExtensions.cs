using System.Reflection;
using FluentValidation;
using Itau.Api.Login;
using Itau.Api.Login.Models;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonsServices(this IServiceCollection services)
        {
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IValidator<LoginDadosRequest>, LoginValidator>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }
            );

            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = "Teste Pratico Itáu",
                        Version = "1.0",
                        Description = "WebAPI para validação de senha"
                    }
                );
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            return services;            
        }
    }
}