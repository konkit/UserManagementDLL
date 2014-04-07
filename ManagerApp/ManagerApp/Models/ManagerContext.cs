using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserDataLib.Models;

namespace ManagerApp.Models
{
    public class ManagerContext : LibContext
    {
        public ManagerContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Item> Item { get; set; }

        

        
    }
}