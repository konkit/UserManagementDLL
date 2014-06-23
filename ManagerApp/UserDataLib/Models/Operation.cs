using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class Operation
    {
        public Operation()
        {
            //Users = new List<UserHasOperation>();
            //OperationGroups = new List<OperationGroupHasOperation>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<OperationGroup> OperationGroups { get; set; }


    }
}
