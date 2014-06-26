using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLib.Models;

namespace UserDataLib.Services
{
    public class GroupManager
    {
        private LibContext db;
        
        public GroupManager(DbContext db)
        {
            this.db = (LibContext)db;
        }

        public List<OperationGroup> DisplayGropus()
        {
            return db.OperationGroup.ToList();
        }

        public void CreateGroup(OperationGroup group)
        {
            if(group != null)
            {
                db.OperationGroup.Add(group);
                db.SaveChanges();
            }
        }

        public OperationGroup FindGroup(int? id)
        {
            return db.OperationGroup.Find(id);
        }

        public void EditGroup(OperationGroup group)
        {
            db.Entry(group).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void AddOperation(int idGroup, int idOperation)
        {
            OperationGroup group = FindGroup(idGroup);
            Operation oper = db.Operation.Where(m => m.Id == idOperation).FirstOrDefault();
            if (group != null)
            {
                group.Operations.Add(oper);
                db.SaveChanges();

            }
        }

        public void DeleteOperation(int idGroup, int idOperation)
        {
            OperationGroup group = FindGroup(idGroup);
            Operation oper = db.Operation.Where(m => m.Id == idOperation).FirstOrDefault();
            if (group != null)
            {
                group.Operations.Remove(oper);
                db.SaveChanges();

            }
        }

        public void DeleteGroup(int id)
        {
            OperationGroup group = FindGroup(id);
            db.OperationGroup.Remove(group);
            db.SaveChanges();
        }
        public void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
        }
    }
    
}
