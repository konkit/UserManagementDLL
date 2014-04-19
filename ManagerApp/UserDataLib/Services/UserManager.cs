﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLib.Models;


namespace UserDataLib.Services
{
    
    public class UserManager
    {
        private LibContext db = new LibContext();

        public List<User> DisplayUser()
        {
            return db.User.ToList();
        }

        public void CreateUser(User user)
        {
            //throw new NotImplementedException();
            if (user!=null)
            {
                User newUser = new User();
                newUser.Id = user.Id;
                newUser.Username = user.Username;
                newUser.Password = user.Password;
                newUser.Salt = user.Salt;
                //newUser.Operations
                db.User.Add(newUser);
                db.SaveChanges();

            }
            
        }

        public void CreateUser(String UserId, String UserPassword, String Role)
        {
            throw new NotImplementedException();
        }

        public User Find(int? id)
        {
            return db.User.Find(id);
        }

        public void EditUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
