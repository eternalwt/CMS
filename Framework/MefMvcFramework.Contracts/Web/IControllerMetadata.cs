namespace MefMvcFramework.Web
{
    /// <summary>
    /// Defines the contract for providing metadata for a controller.
    /// </summary>
    public interface IControllerMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        string Name { get; }
        #endregion
    }
}
