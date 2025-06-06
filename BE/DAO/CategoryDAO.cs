using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.Entities;

namespace BE.DAO
{
    public class CategoryDAO
    {
        private readonly JtContext _context;
        public CategoryDAO(JtContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public bool Create(CreateCategoryRequest category) {
            Category cat = new Category
            {
                Name = category.Name,
                Description = category.Description,
            };

            _context.Categories.Add(cat);
            return _context.SaveChanges() > 0;
        }

        internal List<Category> getCategories()
        {
           return _context.Categories.ToList();
        }
    }
}
