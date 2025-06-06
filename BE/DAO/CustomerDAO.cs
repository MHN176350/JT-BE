using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.Entities;

namespace BE.DAO
{
    public class CustomerDAO
    {
        private readonly JtContext _context;
        public CustomerDAO(JtContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        internal bool CreateCustomer(CreateCustomerRequest request)
        {
            if (_context.Customers.Any(x => x.PhoneNumber.Equals(request.PhoneNumber))){
                return false;

            }
            Customer customer = new Customer
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Points = 0,
                Address= request.Address ?? string.Empty,
                Email= request.Email

            };
            _context.Customers.Add(customer);
            return _context.SaveChanges() > 0;

        }

        internal object GetCustomer()
        {
           return _context.Customers.Select(c => new
            {
                c.Id,
                c.Name,
                c.PhoneNumber,
                c.Address,
                c.Email,
               c.Points
            }).ToList();
        }
    }
}
