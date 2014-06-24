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
