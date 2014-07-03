using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class Users
    {
        public Users()
        {
            UserList = new List<User>();
        }

        [Required]
        public int UserId { get; set; }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Users")]
        public virtual List<User> UserList { get; set; }
    }
}
