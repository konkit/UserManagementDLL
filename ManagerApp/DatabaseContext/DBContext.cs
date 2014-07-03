using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLib.Models;

namespace DatabaseContext
{
    public class DBContext : LibContext
    {
        public DBContext() : base("Entities")
        {
                
        }

        public DbSet<Item> Item { get; set; }

        public DbSet<ItemPossession> ItemPossession { get; set; }
    }
}
