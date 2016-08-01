using log4net;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;

namespace CentauroTech.Utils.WcfLogger
{
    /// <summary>
    /// Message inspector that log the message being passed.
    /// </summary>
    public class LogMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {

        #region Private Fields

        private Guid _identifier = Guid.NewGuid();
        private ILog _logger;
        private LogMessage _logMessage = new LogMessage();

        #endregion Private Fields

        #region Private Properties

        private ILog Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(typeof(LogMessageInspector))); }
            set { _logger = value; }
        }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        /// <param name="correlationState">Correlation state data.</param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            _logMessage.Identifier = _identifier;
            _logMessage.Message = reply;
            LogMessage(_logMessage);
        }

        /// <summary>
        /// Called after an inbound message has been received but before the message is dispatched to the intended operation.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <param name="channel"> The incoming channel.</param>
        /// <param name="instanceContext">The current service instance.</param>
        /// <returns>The object used to correlate state. This object is passed back in the System.ServiceModel.Dispatcher.IDispatchMessageInspector.BeforeSendReply(System.ServiceModel.Channels.Message@,System.Object) method.</returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            _logMessage.Identifier = _identifier;
            _logMessage.Message = request;
            LogMessage(_logMessage);
            return null;
        }

        /// <summary>
        /// Called after the operation has returned but before the reply message is sent.
        /// </summary>
        /// <param name="reply">The reply message. This value is null if the operation is one way.</param>
        /// <param name="correlationState">The correlation object returned from the System.ServiceModel.Dispatcher.IDispatchMessageInspector.AfterReceiveRequest(System.ServiceModel.Channels.Message@,System.ServiceModel.IClientChannel,System.ServiceModel.InstanceContext) method.</param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            _logMessage.Identifier = _identifier;
            _logMessage.Message = reply;
            LogMessage(_logMessage);
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The client object channel.</param>
        /// <returns> The object that is returned as the correlationState argument of the System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object) method. This is null if no correlation state is used.The best practice is to make this a System.Guid to ensure that no two correlationState objects are the same.</returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            _logMessage.Identifier = _identifier;
            _logMessage.Message = request;
            LogMessage(_logMessage);
            return null;
        }

        #endregion Public Methods

        #region Private Methods

        //Supression needed because the log task runs asynchronously and cannot be disposed before the execution ends.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void LogMessage(LogMessage logMessage)
        {
            if (Logger.IsDebugEnabled)
            {
                var logTask = new Task(() =>
                {
                    try
                    {
                        Logger.Debug(logMessage.ToString());
                    }
                    catch (Exception ex)
                    {
                        if (ex is AggregateException)
                            ex = ex.InnerException;

                        Logger.Error("Erro ao gerar log da resposta: " + ex.Message, ex);
                    }
                });
                logTask.Start();
            }
        }

        #endregion Private Methods

    }
}
