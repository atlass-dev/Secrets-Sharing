using Microsoft.AspNetCore.Mvc;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Secrets_Sharing.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var email = User.Identity.Name;
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == email);

            return View(user);
        }
    }
}
