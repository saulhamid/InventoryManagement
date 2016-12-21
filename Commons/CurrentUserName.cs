using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Web;
using System.Security.Permissions;

namespace Commons
{
   public class CurrentUserName
    {
       //public static   System.Security.Principal.IIdentity CurrentUser { get; }
               //public static string UserName { get { return CurrentUser.Name.ToString(); } }
       public static IPrincipal CurrentUser
       {
           get;
           [SecurityPermissionAttribute(SecurityAction.Demand, ControlPrincipal = true)]
           set;
       }
        public static string UserName { get { return CurrentUser.ToString(); } }

    }
}
