using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.FileService.Interfaces;
using Secrets_Sharing.Profile.Interfaces;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Secrets_Sharing.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IFileService _fileService;
        private readonly IResourceRepository _resourceRepository;

        public ProfileController(IProfileService profileService, 
            IFileService fileService, 
            IWebHostEnvironment appEnvironment,
            IResourceRepository resourceRepository)
        {
            _profileService = profileService;
            _appEnvironment = appEnvironment;
            _fileService = fileService;
            _resourceRepository = resourceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userName = User.Identity.Name;
            var response = await _profileService.GetProfile(userName);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult FileLoad()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Resource file = new Resource { Name = uploadedFile.FileName, Path = path };

                var userName = User.Identity.Name;
                var response = await _profileService.GetProfile(userName);

                await _fileService.LoadFile(uploadedFile, response.Data.Id);
            }

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> File(string hash)
        {
            var resources = await _resourceRepository.GetAll();
            var file = resources.FirstOrDefault(f => f.Hash == hash);

            return View(file);
        }
    }
}
