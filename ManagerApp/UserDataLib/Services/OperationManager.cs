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

        public void EditOperation(Operation operation)
        {
            db.Entry(operation).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteOperation(int id)
        {
            Operation operation = db.Operation.Find(id);
            db.Operation.Remove(operation);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
