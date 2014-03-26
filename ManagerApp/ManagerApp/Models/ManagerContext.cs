using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserDataLib.Models;

namespace ManagerApp.Models
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<OperationGroup> OperationGroup { get; set; }
        public DbSet<UserHasOperation> UserHasOperation { get; set; }
        public DbSet<UserHasOperationGroup> UserHasOperationGroup { get; set; }
        public DbSet<OperationGroupHasOperation> OperationGroupHasOperation { get; set; }

        
    }
}