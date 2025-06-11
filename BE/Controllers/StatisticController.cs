using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/stat")]
    [ApiController]
    [Authorize]
    public class StatisticController : ControllerBase
    {
        private readonly IStatistic _statisticService;
        public StatisticController(IStatistic statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("category-revenue")]
        public async Task<IActionResult> GetRevenueByMonth([FromQuery] int warehouseId)
        {
            var result = await _statisticService.BarChartData(DateTime.Now.AddMonths(-1), DateTime.Now, warehouseId);
            return result;
        }
        [HttpGet("pie")]
        public async Task<IActionResult> GetRevenueByCategory([FromQuery] int warehouseId)
        {
            var result = await _statisticService.PieChartData(DateTime.Now.AddMonths(-1), DateTime.Now, warehouseId);
            return result;
        }
      
    }
}
