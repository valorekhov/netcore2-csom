# netcore2-csom
Sample implementation consuming CSOM libraries on the .NET Core 2.0 platform.

The approach is based on using PCL CSOM libraries targeting WindowsStore, and the .NET Standard 1.1 by extension.
This allows to consume CSOM classes without compilation warnings.

In order to support .NET Core 2.0 platform, the Microsoft.SharePoint.Client.Runtime.WindowsStore library is used. 
The library contains a single class NetCoreAppPlatformService responsible for loading platform-specfic DLLs.

Microsoft.SharePoint.Client.Portable.dll and  Microsoft.SharePoint.Client.Runtime.Portable.dll are distributed with the 
Microsoft.SharePointOnline.CSOM NuGet package, and are from the netcore45 target of the package. .NET Core project infrastructure 
does not appear to allow to target a specfific platform from a NuGet package, which is why those DLLs are explicitly linked instead.

## Pros of this approach

* .NET Core 2.0 native support (if you care about it)

## Cons

* PCL libs do not expose synchornous clientContex.Execute() methods, only exposing ExecuteAsync() varians. This will require client
  code to be upaded to work with the async-await pattern.

