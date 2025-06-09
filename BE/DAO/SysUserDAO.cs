using BE.Context;
using BE.Models.Entities;
using BE.Ultility;

namespace BE.DAO
{ 
    public class SysUserDAO
    {
        private readonly JtContext jtContext;
        public SysUserDAO(JtContext context)
        {
            jtContext = context;
        }
        public SysUser? GetById(int id)
        {
            return jtContext.SysUsers.Find(id);
        }
        public SysUser? GetByUsername(string username)
        {
            return jtContext.SysUsers.FirstOrDefault(u => u.Username == username);
        }
        public bool Add(string first, string last, string uname, string pass)
        {
            if(jtContext.SysUsers.Any(x=>x.Username == uname))
            {
                return false;
            }
            var user = new SysUser
            {
                FirstName = first,
                LastName = last,
                Username = uname,
                Password = PasswordHasher.HashPassword(pass),
                Avatar = "def",
                CreatedDate = DateTime.Now,
                LastLogin = DateTime.Now,
                IsAdmin = false,
                IsActive = true,
            };
            jtContext.SysUsers.Add(user);
            return jtContext.SaveChanges() > 0; 
        }
        public void Update(SysUser user)
        {
            jtContext.SysUsers.Update(user);
            jtContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = jtContext.SysUsers.Find(id);
            if (user != null)
            {
                jtContext.SysUsers.Remove(user);
                jtContext.SaveChanges();
            }
        }
        public SysUser Login(string username, string password)
        {
            
            var user = jtContext.SysUsers.FirstOrDefault(u => u.Username == username&&u.IsActive);
            if (user == null||!PasswordHasher.VerifyPassword(password,user.Password))
            {
                return null;
            }
            user.LastLogin = DateTime.Now;
            Update(user); 
            return user;
        }
    }
}
