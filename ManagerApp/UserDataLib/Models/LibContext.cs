using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLib.Models
{
    public class LibContext : DbContext
    {
       
        public LibContext(String connectionString)
            : base(connectionString)
        {

        }
       
        public DbSet<User> User { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<OperationGroup> OperationGroup { get; set; }
    }
}
