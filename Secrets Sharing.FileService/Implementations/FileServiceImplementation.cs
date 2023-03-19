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

        public async Task DeleteFile(string hash)
        {
            var files = await _resourceRepository.GetAll();
            var file = files.FirstOrDefault(f => f.Hash == hash);

            if (file != null)
            {
                await _resourceRepository.Delete(file);

                if (file is File)
                {
                    FileInfo fileInfo = new FileInfo(_webHostEnviroment.WebRootPath + $@"\Files\{file.Name}");

                    if (fileInfo != null)
                        fileInfo.Delete();
                }
            }
                
        }

        public async Task LoadFile(IFormFile uploadedFile, int userId, bool autoRemovable)
        {
            string path = @"\Files\" + uploadedFile.FileName;

            using(var fileStream = new FileStream(_webHostEnviroment.WebRootPath + path, FileMode.Create))
                await uploadedFile.CopyToAsync(fileStream);

            //

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
