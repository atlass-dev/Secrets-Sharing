using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.FileService.Interfaces;
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

        public FileServiceImplementation(IResourceRepository resourceRepository, IWebHostEnvironment webHostEnvironment)
        {
            _resourceRepository = resourceRepository;
            _webHostEnviroment = webHostEnvironment;
        }

        public Task DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public async Task LoadFile(IFormFile uploadedFile)
        {
            string path = "/Files/" + uploadedFile.FileName;

            using(var fileStream = new FileStream(_webHostEnviroment.WebRootPath + path, FileMode.Create))
                await uploadedFile.CopyToAsync(fileStream);

            File file = new File()
            {
                Name = uploadedFile.FileName,
                Hash = "123", // TODO: Add hashing
                Path = _webHostEnviroment.WebRootPath + path,
                Type = Domain.Enums.ResourceType.File
            };

            await _resourceRepository.Create(file);            
        }
    }
}
