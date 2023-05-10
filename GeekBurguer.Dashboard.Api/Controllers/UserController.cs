using GeekBurguer.Dashboard.Api.Models;
using GeekBurguer.Dashboard.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurguer.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("api/api/dashboard")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("usersWithLessOffer")]
        public ActionResult<IEnumerable<User>> GetUsersWithLessOffer()
        {
            var users = _userRepository.GetUsersWithLessOffer();
            return Ok(users);
        }
    }
}