using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserDataLib.Models;
namespace MarketTestApp.Models
{
    public class MarketAppContext : LibContext
    {
        public MarketAppContext()
            : base("Entities")
        {

        }

        public DbSet<Item> Item { get; set; }

        public DbSet<ItemPossession> ItemPossession { get; set; }
    }
}