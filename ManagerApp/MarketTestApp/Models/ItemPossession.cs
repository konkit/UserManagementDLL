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

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Item Item { get; set; }

        public int Quantity { get; set; }
    }
}