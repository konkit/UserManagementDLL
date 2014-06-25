using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        [Display(Name = "Users")]
        public virtual ICollection<User> Users { get; set; }
        [Display(Name = "Group of operations")]
        public virtual ICollection<OperationGroup> OperationGroups { get; set; }
    }    
}
