using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserDataLib.Models;

namespace MarketPlace.Models
{
    public class ItemPossession
    {
        [Key]
        public int Id { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }

        public int Quantity { get; set; }
    }
}