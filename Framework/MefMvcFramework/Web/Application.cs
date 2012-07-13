namespace MefMvcFramework.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Composition;
    using Configuration;

    /// <summary>
    /// Defines a modular application that supports imported types.
    /// </summary>
    public class Application : HttpApplication
    {
        #region Fields
#pragma warning disable 649
        [ImportMany] private IEnumerable<Lazy<IRouteRegistrar, IRouteRegistrarMetadata>> RouteRegistrars;
       // private static IEnumerable<Lazy<IActionVerb, IActionVerbMetadata>> ActionVerbs;

        [Import] private ImportControllerFactory ControllerFactory;
#pragma warning restore 649
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Composer" /> used to compose parts.
        /// </summary>
        public static Composer Composer { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// The start method of the application.
        /// </summary>
        protected void Application_Start()
        {
            // Perform any tasks required before composition.
            PreCompose();

            // Create the composer used for composition.
            Composer = CreateComposer();

            // Compose the application.
            Compose();

            // Set the controller factory.
            ControllerBuilder.Current.SetControllerFactory(ControllerFactory);

            // Set the view engine that supports imported areas.
          //  ViewEngines.Engines.Add(new AreaViewEngine());

            // Initialises the application.
            Initialise();

            // Register MVC routes.
            RegisterRoutes();

        }

	  

        /// <summary>
        /// Initialises the application.
        /// </summary>
        protected virtual void Initialise() { }

        /// <summary>
        /// Creates a <see cref="Composer" /> used to compose parts.
        /// </summary>
        /// <returns></returns>
        protected virtual Composer CreateComposer()
        {
            var composer = new Composer();

            GetDirectoryCatalogs()
                .ForEach(composer.AddCatalog);

            composer.AddExportProvider(
                new DynamicInstantiationExportProvider(),
                (provider, container) => ((DynamicInstantiationExportProvider)provider).SourceProvider = container);

            return composer;
        }

        /// <summary>
        /// Gets an list of <see cref="DirectoryCatalog" />s of configured directories.
        /// </summary>
        /// <returns>An lsit of <see cref="DirectoryCatalog" />s of configured directories.</returns>
        private List<DirectoryCatalog> GetDirectoryCatalogs()
        {
            var list = new List<DirectoryCatalog>();
            string applicationPath = MapPath("~/");

            GetDirectoryCatalogs(MapPath("~/bin")).ForEach(catalog => list.Add(catalog));

			GetDirectoryCatalogs(MapPath("~/Components/Admin")).ForEach(catalog =>
            {
                list.Add(catalog);
                RegisterPath(catalog.FullPath);
            });

            var config = CompositionConfigurationSection.GetInstance();
            if (config != null && config.Catalogs != null)
            {
                config.Catalogs
                    .Cast<CatalogConfigurationElement>()
                    .ForEach(c =>
                    {
                        if (!string.IsNullOrEmpty(c.Path))
                        {
                            string path = c.Path;
                            if (path.StartsWith("~") || path.StartsWith("/"))
                                path = MapPath(path);

                            GetDirectoryCatalogs(path)
                                .ForEach(catalog =>
                                {
                                    list.Add(catalog);
                                    RegisterPath(catalog.FullPath);
                                });
                        }
                    });
            }

            return list;
        }


        /// <summary>
        /// Registers the specified path for probing.
        /// </summary>
        /// <param name="path">The probable path.</param>
        private void RegisterPath(string path)
        {
#pragma warning disable 612,618
            AppDomain.CurrentDomain.AppendPrivatePath(path);
#pragma warning restore 612,618
        }

        /// <summary>
        /// Fired before the application is composed.
        /// </summary>
        protected virtual void PreCompose() { }

        /// <summary>
        /// Composes the application.
        /// </summary>
        protected virtual void Compose()
        {
            if (Composer == null)
                return;

            Composer.Compose(this);

            //ActionVerbs = Composer.ResolveAll<IActionVerb, IActionVerbMetadata>();
        }

        /// <summary>
        /// Registers any routes required by the application.
        /// </summary>
        protected virtual void RegisterRoutes()
        {
            if (RouteRegistrars == null || RouteRegistrars.Count() == 0)
                return;

            var routes = RouteTable.Routes;

            var registrars = RouteRegistrars
                .OrderBy(lazy => lazy.Metadata.Order)
                .Select(lazy => lazy.Value);

            registrars.ForEach(r => r.RegisterIgnoreRoutes(routes));
            registrars.ForEach(r => r.RegisterRoutes(routes));
        }

        /// <summary>
        /// Maps the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path to map.</param>
        /// <returns>The specified virtual path as a mapped path.</returns>
        protected string MapPath(string virtualPath)
        {
            Throw.IfArgumentNullOrEmpty(virtualPath, "virtualPath");

            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        /// <summary>
        /// Gets a set of <see cref="DirectoryCatalog" /> for the specified path and it's immediate child directories.
        /// </summary>
        /// <param name="path">The starting path.</param>
        /// <returns>An <see cref="IEnumerable{DirectoryCatalog}" /> of directory catalogs.</returns>
        protected IEnumerable<DirectoryCatalog> GetDirectoryCatalogs(string path)
        {
            Throw.IfArgumentNullOrEmpty(path, "path");

            List<DirectoryCatalog> list = new List<DirectoryCatalog>();
            list.Add(new DirectoryCatalog(path));

            list.AddRange(
                Directory.GetDirectories(path).Select(directory => new DirectoryCatalog(directory)));

            return list;
        }

		///// <summary>
		///// Gets the available verbs for the given category.
		///// </summary>
		///// <param name="category">The category of verbs to get.</param>
		///// <returns>An enumerable of verbs.</returns>
		//public static IEnumerable<IActionVerb> GetVerbsForCategory(string category)
		//{
		//    Throw.IfArgumentNullOrEmpty(category, "category");

		//    return ActionVerbs
		//        .Where(l => l.Metadata.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase))
		//        .Select(l => l.Value);
		//}
        #endregion
    }
}