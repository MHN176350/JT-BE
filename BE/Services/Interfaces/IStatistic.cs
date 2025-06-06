using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IStatistic
    {
        Task<IActionResult> BarChartData(DateTime startDate, DateTime endDate,int sId);
        Task<IActionResult> PieChartData(DateTime startDate, DateTime endDate, int sId);
    }
}
