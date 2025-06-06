    using BE.Context;
    using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Models.Entities;
    using BE.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Identity.Client;

namespace BE.DAO
{
    public class ImportDAO
    {
        private readonly JtContext _jtContext;
        public ImportDAO(JtContext jtContext)
        {
            _jtContext = jtContext;
        }

        internal bool createImport(CreateImportRequest req, int uid)
        {
            var importInvoice = new ImportInvoice
            {
                SupplierId = req.SupplierId,
                CreatedBy = uid,
                CreatedDate = DateTime.Now,
                Total = 0,
                StorageId = req.StorageId,

            };
            _jtContext.Add(importInvoice);
            _jtContext.SaveChanges();

            var st = _jtContext.Storages.FirstOrDefault(x => x.Id == req.StorageId);
            if (st == null)
                return false;

            var existedItems = _jtContext.Items
                .Where(x => x.StorageId == st.Id)
                .ToDictionary(x => x.ProductId, x => x);

            var productIds = req.list.Select(x => x.ProductId).Distinct().ToList();
            var products = _jtContext.Products
                .Where(x => productIds.Contains(x.Id))
                .ToDictionary(x => x.Id, x => x);

            foreach (var itemReq in req.list)
            {
                if (!products.TryGetValue(itemReq.ProductId, out var pd))
                    continue;

                Item item;
                if (!existedItems.TryGetValue(itemReq.ProductId, out item))
                {
                    item = CreateItem(req.StorageId, itemReq.ProductId, itemReq.Quantity);
                    existedItems[itemReq.ProductId] = item;
                }
                else
                {
                    Importing(item, itemReq.Quantity);
                }

                var newItem = new ImportItem
                {
                    InvoiceId = importInvoice.Id,
                    ItemId = item.Id,
                    Quantity = itemReq.Quantity,
                    UnitPrice = itemReq.UnitPrice,
                    Total = itemReq.Quantity * itemReq.UnitPrice,
                };
                _jtContext.Add(newItem);
                importInvoice.Total += newItem.Total;
            }

            _jtContext.SaveChanges();
            return true;
        }

        public Item CreateItem(int storageID, int productId, long quantity)
        {

            var item = new Item
            {
                StorageId = storageID,
                ProductId = productId,
                Quantity = quantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TotalAmount = quantity
            };
            _jtContext.Items.Add(item);
            _jtContext.SaveChanges();
            return item;
        }

        public void Importing(Item item, int quantity)
        {
            item.Quantity += quantity;
            item.UpdatedAt = DateTime.Now;
            item.TotalAmount += quantity;
            _jtContext.SaveChanges();
        }

        internal object GetImportByWarehouse(int wid)
        {
            var res = _jtContext.ImportInvoices
                  .Include(x => x.CreatedByNavigation)
                  .Include(x => x.Supplier)

                  .Select(x => new
                  {
                      x.Id,
                      CreatedBy = x.CreatedByNavigation.FirstName + " " + x.CreatedByNavigation.LastName,
                      x.CreatedDate,
                      x.Total,
                      Supplier = x.Supplier.Name

                  })
                .ToList();
            return res;
        }

        internal object GetImportDetail(int id)
        {
            var res = _jtContext.ImportItems
                  .Include(x => x.Item)
                  .ThenInclude(x => x.Product)
                  .Where(x => x.InvoiceId == id)
                  .Select(x => new ImportDetailResponse
                  {
                      Id = x.Id,
                    ItemName = x.Item.Product.Name,
                    ItemCode = x.Item.Product.Code,
                      UnitPrice = x.UnitPrice,
                     Quantity = x.Quantity,
                      Total = x.Total
                  })
                  .ToList();
            return res;
        }
    }
}
