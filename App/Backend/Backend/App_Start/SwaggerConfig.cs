using System.Web.Http;
using WebActivatorEx;
using Backend;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Backend
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                              .EnableSwagger(c =>
                              {
                                  c.UseFullTypeNameInSchemaIds();
                                  c.SingleApiVersion("v1", "SwaggerDemoApi");
                                  c.IncludeXmlComments(string.Format(@"{0}\bin\SwaggerApiDoc.xml",
                                                       System.AppDomain.CurrentDomain.BaseDirectory));
                              })
                              .EnableSwaggerUi();
        }
    }
}
