using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLib.Models;

namespace UserDataLib.Services
{
    public class OperationManager
    {
        private LibContext db;

        public OperationManager(DbContext db)
        {
            this.db = (LibContext) db;
        }

        public List<Operation> DisplayOperation()
        {
            return db.Operation.ToList();
        }

        public void CreateOperation(Operation operation)
        {
            if(operation != null)
            {
                db.Operation.Add(operation);
                db.SaveChanges();
            }            
        }
        public Operation FindOperation(int? id)
        {
            return db.Operation.Find(id);
        }
    }
}
