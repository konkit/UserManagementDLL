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
        [Display(Name = "Users")]
        public virtual ICollection<User> Users { get; set; }
        [Display(Name = "Group of operations")]
        public virtual ICollection<OperationGroup> OperationGroups { get; set; }

        public String UsersToString()
        {
            IEnumerator<User> enumerator = Users.GetEnumerator();
            String result = "";
            while (enumerator.MoveNext())
            {
                 if (result.Length > 0)
                 {
                      result += ", ";
                 }
                User operation = enumerator.Current;
                result += operation.Username;
            }
            return result;
        }

        public String OperationGroupsToString()
        {
            IEnumerator<OperationGroup> enumerator = OperationGroups.GetEnumerator();
            String result = "";
            while (enumerator.MoveNext())
            {
                if (result.Length > 0)
                {
                    result += ", ";
                }
                OperationGroup operation = enumerator.Current;
                result += operation.Name;
            }
            return result;
        }
    }
}
