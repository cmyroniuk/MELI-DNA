using DNA_X_Men.Repository;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;

namespace DNA_X_Men
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configura Unity para la inyección de dependencias
            var container = new UnityContainer();
            container.RegisterType<IMutantService, MutantService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMutantRepository, MutantRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Eliminar formateadores XML si deseas solo JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented, // Opcional: para hacer el JSON más legible
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore // Opcional: para ignorar valores nulos
            };
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
