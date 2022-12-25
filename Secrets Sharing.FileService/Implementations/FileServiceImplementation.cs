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

        public FileServiceImplementation(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public Task DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public async Task LoadFile(IFormFile uploadedFile)
        {
            string path = "/Files/" + uploadedFile.FileName;

            using(var fileStream = new FileStream(@"D:\SecretsSharing" + path, FileMode.Create))
                await uploadedFile.CopyToAsync(fileStream);

            File file = new File()
            {
                Name = uploadedFile.FileName,
                Hash = "123", // TODO: Add hashing
                Path = @"D:\SecretsSharing" + path,
                Type = Domain.Enums.ResourceType.File
            };

            await _resourceRepository.Create(file);            
        }
    }
}
