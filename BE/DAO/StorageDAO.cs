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
        private readonly StorageServices _storageServices;
        public StorageDAO(JtContext context, StorageServices storageServices)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _storageServices = storageServices ?? throw new ArgumentNullException(nameof(storageServices));
        }

       

        internal bool AddStorageMember(AddStorageMemberRequest req)
        {
            StorageUser user = new StorageUser()
            {
                UserId = req.UsId,
                RoleId = req.RoleId,
                StorageId = req.StId
            };
            if (_context.StorageUsers.Any(x => x.UserId == user.UserId && x.StorageId == user.StorageId))
            {
                return false;
            }
            _context.StorageUsers.Add(user);
            return _context.SaveChanges()>0;
        }

        internal bool ChangeRole(ChangeRoleRequest req)
        {
           StorageUser? user = _context.StorageUsers.FirstOrDefault(x => x.Id == req.Id);
            if (user != null)
            {
                user.RoleId = req.RoleId;
                return _context.SaveChanges() > 0;
            }
            return false;
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

        internal object GetStorageCodes()
        {
            List<string> codes = _context.Storages.Select(x => x.Code).ToList();
            return codes;
        }

        internal List<StorageMemberResponse> GetStorageMember(string Code)
        {
            List<StorageMemberResponse> res = new List<StorageMemberResponse>();
            var items = _context.StorageUsers.Include(x => x.User).Where(x => x.Storage.Code.ToLower().Equals(Code.ToLower())).ToList();
            foreach (var item in items)
            {
                res.Add(new StorageMemberResponse
                {
                    Id = item.Id,
                    FullName = item.User.FirstName + " " + item.User.LastName,
                    UserName = item.User.Username,
                    Privilage = item.RoleId == 1 ? "Owner" : "Manager",
                    Avatar = _storageServices.GetImageUrl(item.User.Avatar).Result,
                    LastLogin = item.User.LastLogin
                });
            }
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
