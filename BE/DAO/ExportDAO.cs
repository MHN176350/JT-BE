using BE.Context;
using BE.Models.DTO.Request;
using BE.Models.Entities;
using BE.Ultility;
using Microsoft.EntityFrameworkCore;

namespace BE.DAO
{
    public class ExportDAO
    {
        private readonly JtContext _context;
        private readonly UserServices _us;

        public ExportDAO(JtContext context, UserServices userServices)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _us = userServices ?? throw new ArgumentNullException(nameof(userServices));
        }

        public bool CreateExport(CreateExportRequest export)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == export.CustId);
            if (customer == null)
                return false;

            var exportInvoice = new ExportInvoice
            {
                CustId = customer.Id,
                Discount = export.UsePoint ? CalculateDiscountPoint(customer.Points) : 0,
                Total = 0,
                CreatedBy = int.TryParse(_us.GetUserId(), out var userId) ? userId : 0,
                CreatedDate = DateTime.Now,
                StorageId=export.StorageId,
            };

            _context.ExportInvoices.Add(exportInvoice);
            _context.SaveChanges();
            double total = 0;
            foreach (var item in export.items)
            {
                var storageItem = _context.Items.FirstOrDefault(si => si.Id == item.ItemId);
                if (storageItem == null || storageItem.Quantity < item.Quantity)
                    return false;

                storageItem.Quantity -= item.Quantity;

                var exportItem = new ExportItem
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    InvoiceId = exportInvoice.Id,
                    UnitPrice = item.UnitPrice
                };  

                total += item.Quantity * item.UnitPrice;
                customer.Points += (long)(item.Quantity * item.UnitPrice / 1000000);
                _context.ExportItems.Add(exportItem);
            }
            customer.Points -= (long)(exportInvoice.Discount * 10);
            exportInvoice.Total = total;

            if (exportInvoice.Discount > 0)
            {
                double discountAmount = exportInvoice.Total * (10 - exportInvoice.Discount) / 10;
                if (discountAmount > 5000000)
                    discountAmount = 5000000;
                exportInvoice.Total -= discountAmount;
                
            }

            return _context.SaveChanges() > 0;
        }

        private static long CalculateDiscountPoint(long points)
        {
            var discount = points / 10;
            return discount <= 2 ? discount : 2;
        }

        internal object GetExportById(int id)
        {
            var export = _context.ExportInvoices.Include(x=>x.Cust).Include(x=>x.CreatedByNavigation).Where(x=>x.StorageId == id).Select(x => new
            {
                x.Id,
                CustomerName= x.Cust.Name,
                x.Discount,
                x.Total,
                CreatedBy = x.CreatedByNavigation.FirstName+" "+x.CreatedByNavigation.LastName,
                x.CreatedDate,
            }).ToList();
            if(export == null || export.Count == 0)
            {
                return null;
            }
            return export;
        }

        internal object GetExportDetail(int id)
        {
            return _context.ExportItems
                .Include(e => e.Item)
                .ThenInclude(e => e.Product)
                .Where(e => e.InvoiceId == id)
                .Select(e => new
                {
                    e.Id,
                    ProductCode=e.Item.Product.Code,
                    ProductName = e.Item.Product.Name,
                    e.Quantity,
                    e.UnitPrice
                }).ToList();
        }
    }
}
