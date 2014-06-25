using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class UserOperations
    {
        public UserOperations()
        {
            Operations = new List<Operation>();
        }

        [Required]
        public int OperationId { get; set; }
        
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Operations")]
        public List<Operation> Operations { get; set; }
    }
}
