﻿using Microsoft.AspNetCore.Mvc;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.FileService.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Secrets_Sharing.Controllers
{
    public class FileController : Controller
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IFileService _fileService;

        public FileController(IResourceRepository resourceRepository, IFileService fileService)
        {
            _resourceRepository = resourceRepository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string hash)
        {
            var files = await _resourceRepository.GetAll();
            var file = files.Find(x => x.Hash == hash);
        
            if (file != null)
            {
                var downloadedFile = File(System.IO.File.ReadAllBytes(file.Path), "application/octet-stream", file.Name);

                if (file.AutoRemoveable)
                    await _fileService.DeleteFile(file.Hash);

                return downloadedFile;
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadText(string hash) 
        {
            var files = await _resourceRepository.GetAll();
            var text = files.Find(t => t.Hash == hash) as Text;

            if (text != null)
            {
                if (text.AutoRemoveable)
                    await _resourceRepository.Delete(text);

                return View(text);
            }

            return NotFound();
        }
    }
}
