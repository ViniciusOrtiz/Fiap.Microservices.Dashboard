using GeekBurguer.Dashboard.Api.Infra;
using GeekBurguer.Dashboard.Api.Models;
using GeekBurguer.Dashboard.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer.Dashboard.Api.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DashboardContext _context;
        private string _storeName;

        public SalesRepository(DashboardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<SaleResponse> GetOrderAsync(string per, int value)
        {
            var query = _context.SalesRequest.Where(o => o.Value == value);

            if (!string.IsNullOrEmpty(per))
            {
                var perLower = per.ToLower();
                query = perLower switch
                {
                    "hour" => query.Where(o => o.Per.Year == DateTime.Now.Year && o.Per.Month == DateTime.Now.Month && o.Per.Day == DateTime.Now.Day && o.Per.Hour == DateTime.Now.Hour),
                    "day" => query.Where(o => o.Per.Date == DateTime.Today),
                    "week" => query.Where(o => o.Per.Year == DateTime.Now.Year && o.Per.Month == DateTime.Now.Month && o.Per.Day >= DateTime.Now.Day - 7),
                    "month" => query.Where(o => o.Per.Year == DateTime.Now.Year && o.Per.Month == DateTime.Now.Month),
                    "year" => query.Where(o => o.Per.Year == DateTime.Now.Year),
                    _ => throw new ArgumentException("Invalid period parameter.")
                };
            }

            var total = await query.CountAsync();
            var result = await query.SumAsync(o => o.Value);

            if (string.IsNullOrEmpty(_storeName) || (_storeName.ToLower() != "morumbi" && _storeName.ToLower() != "paulista"))
            {
                throw new ArgumentException("Store name is invalid");
            }

            return new SaleResponse
            {
                StoreName = _storeName,
                Total = total,
                Value = value == 0 ? result : (double)value
            };
        }

        public void SetStoreName(string storeName)
        {
            _storeName = storeName;
        }
    }
}