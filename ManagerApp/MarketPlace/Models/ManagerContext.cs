using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserDataLib.Models;

namespace MarketPlace.Models
{
    public class ManagerContext : LibContext
    {
        public ManagerContext()
            : base("Entities")
        {

        }

        public DbSet<Item> Item { get; set; }

        

        
    }
}