using BE.DAO;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BE.Services.Impl
{
    public class StatisticImpl : IStatistic
    {
        private readonly StatisticDAO _statisticDAO;
        public StatisticImpl(StatisticDAO statisticDAO)
        {
            _statisticDAO = statisticDAO;
        }
        public async Task<IActionResult> BarChartData(DateTime startDate, DateTime endDate, int sId)
        {
            if (startDate <= DateTime.MinValue || endDate <= DateTime.MinValue)
            {
                return new OkObjectResult( new ResponseFormat {
                    Data = null, 
                    statusCode = 400, 
                    Message = "Invalid date range provided." });

            }
            List<BarChartResponse> barChartData = _statisticDAO.GetBarChartData(startDate, endDate, sId);
            if (barChartData == null || barChartData.Count == 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "No data found for the given date range.",
                    Data = null
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Bar chart data retrieved successfully.",
                Data = barChartData
            });
        }


        public async Task<IActionResult> PieChartData(DateTime startDate, DateTime endDate, int sId)
        {
            if (startDate <= DateTime.MinValue || endDate <= DateTime.MinValue)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid date range provided.",
                    Data = null
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Pie chart data retrieved successfully.",
                Data = _statisticDAO.getPieChartData(startDate, endDate,sId)
            });
        }
    }
}

