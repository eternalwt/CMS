namespace MefMvcFramework.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Web.Mvc;

    using Composition;

    /// <summary>
    /// Provides creation of controllers using imports.
    /// </summary>
    [Export]
    public class ImportControllerFactory : DefaultControllerFactory
    {
        #region Fields
#pragma warning disable 649
        [ImportMany] private IEnumerable<PartFactory<IController, IControllerMetadata>> ControllerFactories;
#pragma warning restore 649
        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of a controller for the specified name.
        /// </summary>
        /// <param name="requestContext">The current request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>An instance of a controller for the specified name.</returns>
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var factory = ControllerFactories
                .Where(f => f.Metadata.Name.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (factory != null)
                return factory.CreatePart();

            return base.CreateController(requestContext, controllerName);
        }
        #endregion
    }
}
