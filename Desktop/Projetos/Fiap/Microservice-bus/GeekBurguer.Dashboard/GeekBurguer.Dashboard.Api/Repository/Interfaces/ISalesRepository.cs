using GeekBurguer.Dashboard.Api.Models;

namespace GeekBurguer.Dashboard.Api.Repository.Interfaces
{
    public interface ISalesRepository
    {
        Task<SaleResponse> GetOrderAsync(string per, int value);
    }
}
