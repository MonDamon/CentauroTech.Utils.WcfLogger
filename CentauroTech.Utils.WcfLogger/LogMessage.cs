using Newtonsoft.Json;
using System;
using System.ServiceModel.Channels;
using System.Xml;

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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Message Message { get; set; }

        /// <summary>
        /// The body of the message.
        /// </summary>
        public object Body
        {
            get
            {
                using (XmlDictionaryReader reader = Message.GetReaderAtBodyContents())
                {
                    return reader.ReadOuterXml();
                }
            }
        }

        /// <summary>
        /// Override of the ToStringMethod to serialize the object using Json.
        /// </summary>
        /// <returns>The JSON strng of the LogMessage object</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion Public Properties

    }
}
