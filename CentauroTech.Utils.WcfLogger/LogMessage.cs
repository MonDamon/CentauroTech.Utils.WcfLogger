using System;

namespace CentauroTech.Utils.WcfLogger
{
    /// <summary>
    /// The log message that will be created when logging a request.
    /// </summary>
    public class LogMessage
    {

        #region Public Properties

        /// <summary>
        /// The identifier of the request and response.
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// The input request object.
        /// </summary>
        public dynamic InputObject { get; set; }

        /// <summary>
        /// The output request object.
        /// </summary>
        public dynamic OutputObject { get; set; }

        #endregion Public Properties

    }
}
