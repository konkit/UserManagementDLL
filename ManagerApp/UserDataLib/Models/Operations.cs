using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class Operations
    {
        public Operations()
        {
            OperationsList = new List<Operation>();
        }

        [Required]
        public int OperationId { get; set; }
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "Operations")]
        public List<Operation> OperationsList { get; set; }
    }
}
