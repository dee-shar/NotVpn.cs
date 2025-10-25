# NotVpn.cs
Mobile-API for [NotVPN](https://play.google.com/store/apps/details?id=com.notvpn) an Free unconventional VPN, that doesn't typically encrypt all traffic and excessively drain your battery

## Example
```cs
using System;
using NotVpnApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new NotVpn();
            string account = await api.Register();
            Console.WriteLine(account);
        }
    }
}
```
