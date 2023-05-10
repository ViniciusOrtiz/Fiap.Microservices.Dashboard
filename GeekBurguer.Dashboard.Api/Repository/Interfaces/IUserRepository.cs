using GeekBurguer.Dashboard.Api.Models;

namespace GeekBurguer.Dashboard.Api.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsersWithLessOffer();
    }
}
