using System;

namespace CentauroTech.Utils.WcfLogger
{
    /// <summary>
    /// The log message that will be created when logging a request.
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Override of the ToStringMethod to serialize the object using Json.
        /// </summary>
        /// <returns>The JSON strng of the LogMessage object</returns>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        #endregion Public Properties

    }
}
