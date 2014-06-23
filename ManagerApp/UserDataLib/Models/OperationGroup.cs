using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class OperationGroup
    {
        public OperationGroup()
        {
            //Users = new List<UserHasOperationGroup>();
            //Operations = new List<OperationGroupHasOperation>();
        }   

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
