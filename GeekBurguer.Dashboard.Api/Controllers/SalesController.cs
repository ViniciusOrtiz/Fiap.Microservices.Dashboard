using GeekBurguer.Dashboard.Api.Models;
using GeekBurguer.Dashboard.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurguer.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class SaleController : ControllerBase
    {
        private readonly ISalesRepository _saleService;

        public SaleController(ISalesRepository saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("sales")]
        public async Task<SaleResponse> GetOrderAsync(string per, int value)
        {
            return await _saleService.GetOrderAsync(per, value);
        }

        [HttpGet("sales")]
        public async Task<SaleResponse> GetOrderAsync()
        {
            string per = HttpContext.Request.Query["per"].ToString();
            int.TryParse(HttpContext.Request.Query["value"], out var value);

            return await _saleService.GetOrderAsync(per, value);
        }
    }
}