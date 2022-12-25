using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.FileService.Interfaces
{
    public interface IFileService
    {
        public Task LoadFile(IFormFile uploadedFile);
        public Task DeleteFile(string name);
    }
}
