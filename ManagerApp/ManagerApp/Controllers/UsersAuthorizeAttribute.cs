using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UserDataLib.Services
{
    class UsersAuthorizeAttribute : AuthorizeAttribute
    {
        // DO PRZEROBIENIA NA ŁOPATOLOGICZNE I PRZERZUCIĆ DO DLL
        private readonly string userRole;

        public UsersAuthorizeAttribute(string userRole)
        {
            this.userRole = userRole;
        }
 
        protected override bool AuthorizeCore(HttpContextBase httpContext) // metoda, przy pomocy której udzielamy dostępu do akcji bądź nie
        {
            if (httpContext.User.IsInRole(userRole)) // sprawdzamy, czy aktualny user jest adminem
            {
                return true; // jeśli tak - uzyskuje on dostęp
            }
 
            return false; // jeśli nie, błąd 401 - nieuprawnionym wstęp wzbroniony
        }
    }
}
