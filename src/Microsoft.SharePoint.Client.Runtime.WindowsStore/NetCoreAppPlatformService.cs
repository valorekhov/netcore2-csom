using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.SharePoint.Client
{
    public sealed class NetCoreAppPlatformService : PlatformService
    {
        readonly static Regex nameRegex = new Regex(@"^Microsoft\.(Office|SharePoint).+\.Portable\.dll$", RegexOptions.Compiled | RegexOptions.Compiled);

        protected override Task<IEnumerable<Assembly>> GetClientTypeAssembliesAsync()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            var di = new DirectoryInfo(path);
            List<Assembly> list = new List<Assembly>();

            foreach (var file in di.GetFiles("*.dll").Where(fi=>nameRegex.IsMatch(fi.Name)))
            {
                try
                {
                    Assembly item = Assembly.LoadFrom(file.FullName);
                    list.Add(item);
                }
                catch (Exception)
                {
                }
            }
            return Task.FromResult<IEnumerable<Assembly>>(list);
        }
    }
}
