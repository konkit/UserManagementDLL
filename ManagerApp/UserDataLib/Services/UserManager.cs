using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLib.Models;
using System.Security.Cryptography;
using System.Data.Entity;


namespace UserDataLib.Services
{
    public class UserManager
    {
        private static User userValue;

        private const int SALT_BYTE_SIZE = 24;
        private const int HASH_BYTE_SIZE = 24;
        private const int PBKDF2_ITERATIONS = 1000;
        
        private const int ITERATION_INDEX = 0;
        private const int SALT_INDEX = 1;
        private const int PBKDF2_INDEX = 2;

        private LibContext db;

        public UserManager(DbContext db)
        {
            this.db = (LibContext) db;
        }

        public List<User> DisplayUser()
        {
            string a = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return db.User.ToList();
        }

        public bool LoginUserIsValid(LoginViewModel user)
        {
            if(user !=null)
            {
                var query = (from u in db.User
                             where u.Username == user.Username
                             select u).FirstOrDefault();

                if (query != null)
                {
                    if (ValidatePassword(user.Password, CreateHash(query.Password, query.Salt)))
                    {
                        userValue = query;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }    
            }
            return false;
        }

        public bool UserNameIsNoExists(RegisterViewModel user)
        {
            if(user!=null)
            {
                var query = (from u in db.User
                             where u.Username == user.Username
                             select u).FirstOrDefault();
                if(query==null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool ChangeActiveAccount(LoginViewModel user)
        {
            
            var model = db.User.FirstOrDefault(x => x.Username == user.Username);
            if(model.data<DateTime.UtcNow)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CreateUser(RegisterViewModel user)
        {
            Operation oper = db.Operation.Where(m => m.Id == 2).FirstOrDefault();
            if (user!=null)
            {    
                User newUser = new User();
                newUser.Id = user.Id;
                newUser.Username = user.Username;
                newUser.Salt = CreateSalt();
                newUser.Password = user.Password;
                newUser.IsActive = true;
                newUser.data = DateTime.UtcNow.AddYears(1);
                
                newUser.Operations = new List<Operation>();
                newUser.Operations.Add(oper);               
                
                db.User.Add(newUser);
                db.SaveChanges();
                
            }
            
        }   


        public static string CreateSalt()
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);
            return Encoding.UTF8.GetString(salt);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for(int i=0; i<a.Length && i<b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

        public static string CreateHash(string password, string salt)
        {
            byte[] saltByte;
            if(salt == null)
            {
                saltByte = Array.ConvertAll(CreateSalt().ToCharArray(), (x) => (byte)x);
            }
            else
            {
                saltByte = Array.ConvertAll(salt.ToCharArray(), (x) => (byte)x);
            }
            
            byte[] hash = PBKDF2(password, saltByte, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" + Convert.ToBase64String(saltByte) + ":" + Convert.ToBase64String(hash);
        }       

        public User Find(int? id)
        {
            return db.User.Find(id);
        }

        public void ChangeActivation(User user)
        {
            var obj = Find(user.Id);
            if (obj == null)
                return;
            obj.data = user.data;
            if(obj.data>DateTime.UtcNow)
            {
                obj.IsActive = true;
            }
            else
            {
                obj.IsActive = false;
            }
            
            db.SaveChanges();
        }

        public void EditUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void AddOperation(int idUser, int idOperation)
        {
            User user = Find(idUser);
            Operation oper = db.Operation.Where(m => m.Id == idOperation).FirstOrDefault();
            if (user != null)
            {
                user.Operations.Add(oper);
                db.SaveChanges();

            }
        }

        public void DeleteOperation(int idUser, int idOperation)
        {
            User user = Find(idUser);
            Operation oper = db.Operation.Where(m => m.Id == idOperation).FirstOrDefault();
            if (user != null)
            {
                user.Operations.Remove(oper);
                db.SaveChanges();

            }
        }

        public void AddGroup(int idUser, int idGroup)
        {
            User user = Find(idUser);
            OperationGroup group = db.OperationGroup.Where(m => m.Id == idGroup).FirstOrDefault();
            if (user != null)
            {
                user.OperationGroups.Add(group);
                foreach(Operation oper in group.Operations)
                {
                    if(!user.Operations.Contains(oper))
                    {
                        user.Operations.Add(oper);
                    }
                }
                db.SaveChanges();

            }
        }

        public void DeleteGroup(int idUser, int idGroup)
        {
            User user = Find(idUser);
            OperationGroup group = db.OperationGroup.Where(m => m.Id == idGroup).FirstOrDefault();
            if (user != null)
            {
                user.OperationGroups.Remove(group);
                foreach (Operation oper in group.Operations)
                {
                    if (user.Operations.Contains(oper))
                    {
                        user.Operations.Remove(oper);
                    }
                }
                db.SaveChanges();

            }
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
        
        public User GetUser()
        {
            return userValue;
        }

        public User getUser(string username, string password)
        {
            var user = db.User.FirstOrDefault(x => x.Username == username);

            if (user == null || user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            return user;
        }
    }
}
