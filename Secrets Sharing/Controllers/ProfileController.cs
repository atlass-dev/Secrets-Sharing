using Microsoft.AspNetCore.Mvc;

namespace Secrets_Sharing.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
