using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.DAO
{
    public class ProductDAO
    {
        private readonly JtContext _context;
        public ProductDAO(JtContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public bool CreateProduct(CreateProductRequest product)
        {
            if(_context.Products.Any(p => p.Code == product.Code))
                return false;   
            Product pd= new Product
            {
                Name = product.Name,
                Code = product.Code,
                Image = product.Image,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Descriptions= product.Description,
                CatId= product.CatId
            };
            _context.Products.Add(pd);
            return _context.SaveChanges() > 0;
        }

        internal List<ProductResponse> GetProducts()
        {
           return _context.Products.Include(x=>x.Cat).Select(p => new ProductResponse { 
               id = p.Id,
               code = p.Code,
                name = p.Name,
                image = p.Image,
                createdDate = p.CreatedDate,
                updatedDate = p.UpdatedDate,
                description = p.Descriptions,
                categoryName =p.Cat.Name
            }).ToList();
        }
    }
}
