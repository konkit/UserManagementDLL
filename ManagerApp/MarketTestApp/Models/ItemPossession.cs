using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserDataLib.Models;

namespace MarketTestApp.Models
{
    public class ItemPossession
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }
    }
}