using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace UserDataLib.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string operation)
        {
            if (operations.Any(r => operation.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInGroup(string group)
        {
            string[] gropus = group.Split(',');
            foreach(string word in gropus)
            {
                if (groups.Any(g => word.Contains(g)))
                {
                    return true;
                }
               
            }
            return false;
        }
 
        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string Username { get; set; }        
        public string[] operations { get; set; }
        public string[] groups { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }        
        public string[] operations { get; set; }
        public string[] groups { get; set; }
    }
    
}