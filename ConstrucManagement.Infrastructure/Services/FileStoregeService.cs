﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Infrastructure.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(byte[] content, string extension, string containerName);
        Task<string> EditFileAsync(byte[] content, string extension, string containerName, string fileRoute);
        Task DeleteFileAsync(string fileRoute, string containerName);
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFileAsync(byte[] content, string extension, string containerName)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string savingPath = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(savingPath, content);

            var currentUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            return Path.Combine(currentUrl, containerName, fileName).Replace("\\", "/");
        }

        public async Task<string> EditFileAsync(byte[] content, string extension, string containerName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFileAsync(fileRoute, containerName);
            }

            return await SaveFileAsync(content, extension, containerName);
        }

        public async Task DeleteFileAsync(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return;
            }

            var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(_env.WebRootPath, containerName, fileName);

            if (File.Exists(fileDirectory))
            {
                await Task.Run(() => File.Delete(fileDirectory));
            }
        }
    }
}
