pwinty-dotnet
=============

Pwinty .NET Client Library - a C# .NET client for http://www.pwinty.com

Configuration
-----------

Pwinty-dotnet picks up your API keys from the application configuration file:

```xml
<configuration>
  <appSettings>
    <add key="Pwinty-MerchantId" value="abc"/>
    <add key="Pwinty-REST-API-Key" value="abc"/>
    <add key="Pwinty-Base-Url" value="https://sandbox.pwinty.com"/>
  </appSettings>
</configuration>
```

Usage
-----------
See the Pwinty.Client.Test project for example usage