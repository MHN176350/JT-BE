using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BE.DAO
{
    public class StorageDAO
    {
        private readonly JtContext _context;
        public StorageDAO(JtContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

       

        internal bool AddStorageMember(AddStorageMemberRequest req)
        {
            StorageUser user = new StorageUser()
 ;
            user.UserId=req.usId ;
            user.RoleId = 2;
            user.StorageId = req.stId;
            if (_context.StorageUsers.Any(x => x.UserId == user.UserId && x.StorageId == user.StorageId))
            {
                return false;
            }
            _context.StorageUsers.Add(user);
            return _context.SaveChanges()>0;
        }
     

        internal bool CreateStorage(string location, string code, int usId)
        {
            if(_context.Storages.Any(x => x.Code == code))
            {
                return false; 
            }
            Storage storage = new Storage
            {
                Location = location,
                Code = code,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _context.Storages.Add(storage);
            if (_context.SaveChanges() > 0) { 
            _context.StorageUsers.Add(new StorageUser
            {
                UserId = usId,
                StorageId = storage.Id,
                RoleId = 1
            });
                return _context.SaveChanges() > 0;
            }
            return false;

        }

        internal List<StorageResponse> GetStorageByUserId(int uid)
        {
          List<StorageUser> ls=_context.StorageUsers.Include(x => x.Storage).Include(x=>x.User).Where(x => x.UserId == uid).ToList();
            return ls.Select(s => new StorageResponse
            {
                Id = s.Id,
                OwnerName=s.User.FirstName + " " + s.User.LastName,
                Location = s.Storage.Location,
                Code = s.Storage.Code,
                CreatedDate = s.Storage.CreatedDate,
                ItemCount = _context.Items.Count(i => i.StorageId == s.StorageId),
                UpdatedDate = s.Storage.UpdatedDate
            }).ToList();
        }

        internal object GetStorageMember(int stId)
        {
            List<StorageMemberResponse> res = _context.StorageUsers.Where(x => x.StorageId == stId).Select(x => new StorageMemberResponse
            {
                Id = x.Id,
                FullName = x.User.FirstName + " " + x.User.LastName,
                UserName = x.User.Username,
                Privilage = x.RoleId == 1 ? "Owner" : "Manager",
            }).ToList();
            return res;
        }

        internal bool updateStorage(UpdateStorageRequest req, int uid)
        {
          
            if (_context.StorageUsers.Any(x => x.Id == uid && x.StorageId == req.StorageID && x.RoleId == 1)&&_context.Storages.Any(x=>x.Id==req.StorageID))
            {
                var storage = _context.Storages.FirstOrDefault(x=>x.Id==req.StorageID);
                if(storage != null)
                {
                    storage.Location = req.Location;
                    return _context.SaveChanges()>0;
                }
            }
            return false;
        }
    }
}
