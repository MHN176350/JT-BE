using BE.Context;
using BE.Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.DAO
{
    public class StatisticDAO
    {
        private readonly JtContext _context;

        public StatisticDAO(JtContext context)
        {
            _context = context;
        }

        internal List<BarChartResponse> GetBarChartData(DateTime startDate, DateTime endDate, int sId)
        {
            List<BarChartResponse> res = new List<BarChartResponse>();
            var cat= _context.Categories.ToList();
            foreach (var category in cat)
            {
                
                double exportSum = _context.ExportInvoices
                    .Where(e => e.StorageId == sId && e.CreatedDate >= startDate && e.CreatedDate <= endDate && e.ExportItems
                    .Any(i => i.Item.Product.CatId == category.Id))
                    .Sum(e => e.ExportItems.Sum(i => i.UnitPrice * i.Quantity));
                double importSum = _context.ImportInvoices
                    .Where(i => i.StorageId == sId && i.CreatedDate >= startDate && i.CreatedDate <= endDate && i.ImportItems
                    .Any(it => it.Item.Product.CatId == category.Id))
                    .Sum(i => i.ImportItems.Sum(it => it.UnitPrice * it.Quantity));
                res.Add(new BarChartResponse
                {
                    Category = category.Name,
                    Revenue = exportSum - importSum
                }); 
            }
            return res;
        }

        internal List<PieChartResponse> getPieChartData(DateTime startDate, DateTime endDate, int sId )
        {
            List<PieChartResponse> res = new List<PieChartResponse>();
            var productList = _context.Products.ToList();
            foreach (var prod in productList)
            {
                res.Add(new PieChartResponse
                {
                    ProductName = prod.Name,
                    Quantity = _context.ExportInvoices
                        .Where(e =>e.StorageId==sId&& e.CreatedDate >= startDate && e.CreatedDate <= endDate && e.ExportItems.Any(i => i.Item.Product.Id == prod.Id))
                        .Sum(e => e.ExportItems.Sum(i => i.Quantity))
                });
            }
            return res.ToList();
        }
    }
}
