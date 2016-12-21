using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.


namespace Common
{
   public class GetIpAddress
    {
       public static string GetLocalIPAddress()
       {
           var host = Dns.GetHostEntry(Dns.GetHostName());
           foreach (var ip in host.AddressList)
           {
               if (ip.AddressFamily == AddressFamily.InterNetwork)
               {
                   return ip.ToString();
               }
           }
           throw new Exception("Local IP Address Not Found!");
       }
    }
}
