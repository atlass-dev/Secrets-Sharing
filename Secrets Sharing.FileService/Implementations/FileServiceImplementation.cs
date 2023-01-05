using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Enums;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.FileService.Interfaces;
using Secrets_Sharing.Hasher.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Secrets_Sharing.Domain.Models.File;

namespace Secrets_Sharing.FileService.Implementations
{
    public class FileServiceImplementation : IFileService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IHasher _hasher;

        public FileServiceImplementation(IResourceRepository resourceRepository, 
            IWebHostEnvironment webHostEnvironment,
            IHasher hasher)
        {
            _resourceRepository = resourceRepository;
            _webHostEnviroment = webHostEnvironment;
            _hasher = hasher;
        }

        public Task DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public async Task LoadText(string title, string text, bool autoRemovable)
        {
            string path = "/Files/" + title;

            using(StreamWriter sw = new StreamWriter(_webHostEnviroment.WebRootPath + path))
            {
                await sw.WriteAsync(text);
            }
        }

        public async Task LoadFile(IFormFile uploadedFile, int userId, bool autoRemovable)
        {
            string path = "/Files/" + uploadedFile.FileName;

            using(var fileStream = new FileStream(_webHostEnviroment.WebRootPath + path, FileMode.Create))
                await uploadedFile.CopyToAsync(fileStream);

            File file = new File()
            {
                Name = uploadedFile.FileName,
                Hash = _hasher.GetHash($"{userId}{uploadedFile.FileName}"),
                Path = _webHostEnviroment.WebRootPath + path,
                Type = ResourceType.File,
                AutoRemoveable = autoRemovable,
                UserId = userId
            };

            await _resourceRepository.Create(file);            
        }

        public async Task LoadFile(Text text, int userId)
        {
            text.Hash = _hasher.GetHash($"{userId}{text.Name}");
            text.Type = ResourceType.Text;
            text.UserId = userId;

            await _resourceRepository.Create(text);
        }
    }
}
