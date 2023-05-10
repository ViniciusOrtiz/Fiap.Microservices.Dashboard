using GeekBurguer.Dashboard.Api.Infra;
using GeekBurguer.Dashboard.Api.Models;
using GeekBurguer.Dashboard.Api.Repository.Interfaces;

namespace GeekBurguer.Dashboard.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DashboardContext _context;

        public UserRepository(DashboardContext context)
        {
            _context = context;
        }

        public List<User> GetUsersWithLessOffer()
        {
            return _context.User
                .Where(u => u.Restrictions != null && u.Restrictions.Length <= 2)
                .Select(u => new User { Users = u.Users, Restrictions = u.Restrictions })
                .ToList();
        }
    }
}