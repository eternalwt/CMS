namespace MefMvcFramework.Web
{
    using System;
    using System.ComponentModel.Composition;
    using System.Web.Mvc;

    /// <summary>
    /// Exports a controller.
    /// </summary>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportControllerAttribute : ExportAttribute, IControllerMetadata
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of the <see cref="ExportControllerAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        public ExportControllerAttribute(string name) : base(typeof(IController))
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Controller name cannot be null or empty.");

            this.Name = name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public string Name { get; private set; }
        #endregion
    }
}
