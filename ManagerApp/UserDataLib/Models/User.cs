using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class User
    {
        public User()
        {
            //OperationGroups = new List<UserHasOperationGroup>();
            //Operation = new List<UserHasOperation>();
        }

        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public ICollection<OperationGroup> OperationGroups { get; set; }
    }
}
