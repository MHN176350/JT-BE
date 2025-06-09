using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.Entities;
using BE.Services.Interfaces;
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

        

        internal async Task<bool> ChangePassword(int id, string oldPassword, string newPassword)
        {
          const int minPasswordLength = 6;
            if (newPassword.Length < minPasswordLength)
            {
                throw new ArgumentException($"New password must be at least {minPasswordLength} characters long.");
            }
            var user = await jtContext.SysUsers.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            if (!PasswordHasher.VerifyPassword(oldPassword, user.Password))
            {
                return false;
            }
            user.Password = PasswordHasher.HashPassword(newPassword);
           return jtContext.SaveChanges() > 0;  
        }

        internal bool LockUser(int userId)
        {
           SysUser u=jtContext.SysUsers.Find(userId);
            if (u == null)
            {
                return false;
            }
            if (u.IsActive) { u.IsActive = false; } else { u.IsActive = true; } 
            jtContext.SysUsers.Update(u);
            return jtContext.SaveChanges() > 0;
        }

        internal bool UpdateProfile(UpdateProfileRequest updateProfileRequest)
        {
           SysUser u=jtContext.SysUsers.FirstOrDefault(x=>x.Id==updateProfileRequest.Id);
            if (u == null)
            {
                return false;
            }
            u.Avatar=updateProfileRequest.Avatar;
            return jtContext.SaveChanges() > 0;
        }
    }
}
