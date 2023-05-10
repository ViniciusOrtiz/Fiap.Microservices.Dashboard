using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekBurguer.Dashboard.Api.Infra;

public class ApiClient
{
    private readonly string _url;
    private const string OrderEndpoint = "OrderChanged";

    public ApiClient(IConfiguration configuration)
    {
        _url = configuration.GetValue<string>("Services:Products:ApiBaseAddress");
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _url.AppendPathSegment(ProductsEndpoint).GetJsonAsync<List<Product>>();
    }
}