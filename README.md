# CentauroTech.Utils.WcfLogger
Nuget package to log requests made during WCF request.

#### Status
Branches: &nbsp;&nbsp;&nbsp; [![Build status](https://ci.appveyor.com/api/projects/status/dtpi1509g3i2lxux/branch/master?svg=true)](https://ci.appveyor.com/project/jmtvms/centaurotech-utils-wcflogger/branch/master)

Master: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [![Build status](https://ci.appveyor.com/api/projects/status/dtpi1509g3i2lxux?svg=true)](https://ci.appveyor.com/project/jmtvms/centaurotech-utils-wcflogger)

#### Nuget package installation:
To install CentauroTech.Utils.WcfLogger, run the following command in the Package Manager Console
```
	PM> Install-Package CentauroTech.Utils.WcfLogger
```
More information about the package, please visit:
https://www.nuget.org/packages/CentauroTech.Utils.WcfLogger

#### Usage:
Add the message handler to the HttpConfiguration of you web site. The logs will appear wherever your log4net logs are being presented by your app.

```csharp
using (var client = new WcfWebService())
{
  var logger = new CentauroTech.Utils.WcfLogger.LogBehaviorAttribute();                   
  client.Endpoint.EndpointBehaviors.Add(logger);
}
```

##### References:
log4net - https://logging.apache.org/log4net     
Newtonsoft JSON - http://www.newtonsoft.com/json

