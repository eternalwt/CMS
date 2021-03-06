﻿namespace MefMvcFramework
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    static partial class Throw
    {
        #region Methods
        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if the specified argument is null.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        [DebuggerStepThrough]
        public static void IfArgumentNull(object argument, string argumentName, Func<Exception, Exception> modifier = null)
        {
            if (argument == null)
                ThrowInternal(
                    new ArgumentNullException(argumentName),
                    modifier);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if the specified argument is null or equal to <see cref="String.Empty" />.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        [DebuggerStepThrough]
        public static void IfArgumentNullOrEmpty(string argument, string argumentName, Func<Exception, Exception> modifier = null)
        {
            if (string.IsNullOrEmpty(argument))
                ThrowInternal(
                    new ArgumentException(
                        string.Format(CultureInfo.CurrentUICulture, Resources.Throw.ArgumentNullOrEmpty, argumentName)),
                    modifier);
        }
        #endregion
    }
}