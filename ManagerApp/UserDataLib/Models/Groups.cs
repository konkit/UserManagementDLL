using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class Groups
    {
        public Groups()
        {
            GroupsList = new List<OperationGroup>();
        }

        [Required]
        public int GroupId { get; set; }
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "Groups")]
        public List<OperationGroup> GroupsList { get; set; }
    }
}
