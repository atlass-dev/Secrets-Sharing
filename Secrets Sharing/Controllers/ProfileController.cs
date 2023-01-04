using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.FileService.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Secrets_Sharing.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;

        public ProfileController(IUserRepository userRepository, IFileService fileService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
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

        [HttpGet]
        public IActionResult LoadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFile(IFormFile uploadedFile, bool autoRemovable)
        {
            var email = User.Identity.Name;
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == email);

            await _fileService.LoadFile(uploadedFile, user.Id, autoRemovable);

            return RedirectToAction("Index", "Profile");
        }
    }
}
