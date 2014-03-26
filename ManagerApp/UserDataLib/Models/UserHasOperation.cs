using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class UserHasOperation
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Operation")]
        public int OperationId { get; set; }
        public Operation Operation { get; set; }
    }
}
