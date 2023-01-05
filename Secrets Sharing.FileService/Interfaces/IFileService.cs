using Microsoft.AspNetCore.Http;
using Secrets_Sharing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.FileService.Interfaces
{
    public interface IFileService
    {
        public Task LoadFile(IFormFile uploadedFile, int userId, bool autoRemovable);
        public Task LoadFile(Text text, int userId);
        public Task DeleteFile(string name);
    }
}
