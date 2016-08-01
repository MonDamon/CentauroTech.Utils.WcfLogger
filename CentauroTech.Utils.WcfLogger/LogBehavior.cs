using log4net;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace CentauroTech.Utils.WcfLogger
{
    /// <summary>
    /// The behaviour that should be used to log the messages on a WCF communication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LogBehaviorAttribute : Attribute, IServiceBehavior, IEndpointBehavior
    {

        #region Private Fields

        private bool? _logExecution;
        private ILog _logger;

        #endregion Private Fields

        #region Private Properties

        private bool LogExecution
        {
            get
            {
                if (!_logExecution.HasValue)
                {
                    var setting = System.Configuration.ConfigurationManager.AppSettings["WcfLogger.Internal.Debug"];
                    bool logExecution = false;

                    if (!bool.TryParse(setting, out logExecution))
                        logExecution = false;

                    _logExecution = logExecution;
                }
                return _logExecution.Value;
            }
        }

        private ILog Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(typeof(LogBehaviorAttribute))); }
            set { _logger = value; }
        }

        #endregion Private Properties

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LogBehaviorAttribute()
        {
            if (LogExecution)
                Logger.Debug("Constructing LogBehaviorAttribute.");
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        /// <remarks>Not implemented.</remarks>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

            if (LogExecution)
                Logger.Debug("Executing: AddBindingParameters(ServiceEndpoint, BindingParameterCollection)");
        }


        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

            if (LogExecution)
                Logger.Debug("Executing: AddBindingParameters(ServiceDescription, ServiceHostBase, Collection<ServiceEndpoint>, BindingParameterCollection)");
        }

        /// <summary>
        /// Implements a modification or extension of the client across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that is to be customized.</param>
        /// <param name="clientRuntime">The client runtime to be customized.</param>
        /// <exception cref="ArgumentNullException">If clientRuntime is null</exception>
        /// <remarks>Not implemented.</remarks>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            if (LogExecution)
                Logger.Debug("Executing: ApplyClientBehavior(ServiceEndpoint, ClientRuntime)");

            if (clientRuntime == null)
                throw new ArgumentNullException("clientRuntime");

            clientRuntime.MessageInspectors.Add(new LogMessageInspector());
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase"> The host that is currently being built.</param>
        /// <exception cref="ArgumentNullException">If serviceHostBase is null</exception>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (LogExecution)
                Logger.Debug("Executing: ApplyDispatchBehavior(ServiceDescription, ServiceHostBase)");

            if (serviceHostBase == null)
                throw new ArgumentNullException("serviceHostBase");

            serviceHostBase.ChannelDispatchers
                .Where(cd => cd is ChannelDispatcher)
                .Select(cd => cd as ChannelDispatcher)
                .ToList().ForEach(cd =>
                {
                    foreach (EndpointDispatcher endpointDispatcher in cd.Endpoints)
                    {
                        var inspector = new LogMessageInspector();
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
                    }
                });
        }

        /// <summary>
        /// Implements a modification or extension of the service across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.</param>
        /// <remarks>Not implemented.</remarks>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            if (LogExecution)
                Logger.Debug("Executing: ApplyDispatchBehavior(ServiceEndpoint, EndpointDispatcher)");

            if (endpointDispatcher == null)
                throw new ArgumentNullException("endpointDispatcher");

            var inspector = new LogMessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        /// <summary>
        /// Implement to confirm that the endpoint meets some intended criteria.
        /// </summary>
        /// <param name="endpoint">The endpoint to validate.</param>
        /// <remarks>Not implemented.</remarks>
        public void Validate(ServiceEndpoint endpoint)
        {
            if (LogExecution)
                Logger.Debug("Executing: Validate(ServiceEndpoint)");
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        /// <remarks>Not implemented.</remarks>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (LogExecution)
                Logger.Debug("Executing: Validate(ServiceDescription, ServiceHostBase)");
        }

        #endregion Public Methods

    }
}
