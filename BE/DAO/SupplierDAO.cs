using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Models.Entities;

namespace BE.DAO
{
    public class SupplierDAO
    {
        private readonly JtContext _jtContext;
        public SupplierDAO(JtContext jtContext)
        {
            _jtContext = jtContext;
        }
        internal bool CreateSupplier(CreateSupplierRequest createSupplierRequest)
        {
            Supplier sup = new Supplier
            {
                Address = createSupplierRequest.Address,
                Name = createSupplierRequest.Name,
                PhoneNumber = createSupplierRequest.PhoneNumber,
                CreatedDate = DateTime.Now
            };
            _jtContext.Add(sup);
            return _jtContext.SaveChanges() > 0;
        }

        internal object GetSup()
        {
            List<SupplierResponse> res = _jtContext.Suppliers.Select(x => new SupplierResponse
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                AddedDate = x.CreatedDate,
            }).ToList();
            return res;
        }
    }
}
