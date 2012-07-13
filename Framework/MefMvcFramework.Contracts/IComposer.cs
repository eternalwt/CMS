namespace MefMvcFramework
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the required contract for implementing a composer
    /// </summary>
    public interface IComposer
    {
        #region Methods
        /// <summary>
        /// Composes the specified object.
        /// </summary>
        /// <param name="object">The object to be composed.</param>
        void Compose(object @object);

        /// <summary>
        /// Gets an instance of the specified type from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <returns>The resolved instance.</returns>
        T Resolve<T>();

        /// <summary>
        /// Gets an instance of the specified type from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>The resolved instance.</returns>
        T Resolve<T>(string contractName);

        /// <summary>
        /// Gets an instance of <see cref="Lazy{T,TMetadata}" /> for the specified trip from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <returns>A <see cref="Lazy{T,TMetadata}" /> that allows lazy-loading.</returns>
        Lazy<T, TMetadata> Resolve<T, TMetadata>();

        /// <summary>
        /// Gets an instance of <see cref="Lazy{T,TMetadata}" /> for the specified trip from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>A <see cref="Lazy{T,TMetadata}" /> that allows lazy-loading.</returns>
        Lazy<T, TMetadata> Resolve<T, TMetadata>(string contractName);

        /// <summary>
        /// Gets all instances of the specified type from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <returns>An enumerable of resolved instances.</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Gets all instances of the specified type from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>An enumerable of resolved instances.</returns>
        IEnumerable<T> ResolveAll<T>(string contractName);

        /// <summary>
        /// Gets all instances of <see cref="Lazy{T,TMetadata}" /> of the specified type from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <returns>An enumerable of <see cref="Lazy{T,TMetadata}" />.</returns>
        IEnumerable<Lazy<T, TMetadata>> ResolveAll<T, TMetadata>();

        /// <summary>
        /// Gets all instances of <see cref="Lazy{T,TMetadata}" /> of the specified type from the <see cref="IComposer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>An enumerable of <see cref="Lazy{T,TMetadata}" />.</returns>
        IEnumerable<Lazy<T, TMetadata>> ResolveAll<T, TMetadata>(string contractName);
        #endregion
    }
}
