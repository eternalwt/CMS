namespace MefMvcFramework.Web
{
    /// <summary>
    /// Defines an action verb.
    /// </summary>
    public interface IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name of the verb.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        string Action { get; }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        string Controller { get; }
        #endregion
    }
}