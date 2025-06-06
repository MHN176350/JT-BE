using BE.Context;
using BE.Models.DTO.Response;
using BE.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.DAO
{
    public class ItemDAO
    {
        private readonly JtContext _context;
        public ItemDAO(JtContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public bool CreateItem(int storageID, int productId, long quantity)
        {
           Item item = new Item
            {
                StorageId = storageID,
                ProductId = productId,
                Quantity = quantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TotalAmount =quantity
           };
            _context.Items.Add(item);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateItem(int itemId, long quantity)
        {
            var item = _context.Items.Find(itemId);
            if (item == null) return false;
            item.Quantity = quantity;
            item.UpdatedAt = DateTime.Now;
            _context.Items.Update(item);
            return _context.SaveChanges() > 0;
        }

        internal List<ItemResponse> GetItemsByStorageId(int storageId)
        {
           List<ItemResponse> items = _context.Items
                .Where(i => i.StorageId == storageId)
                .Include(i => i.Product)
                .Select(i => new ItemResponse
                {
                    Id = i.Id,
                    quantity = i.Quantity,
                    createdAt = i.CreatedAt,
                    updatedAt = i.UpdatedAt,
                    productName = i.Product.Name,
                    productImage = i.Product.Image,
                    totalAmount = i.TotalAmount
                }).ToList();
            return items;
        }
    }
}
