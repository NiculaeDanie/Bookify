using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IAzureStorage
    {
        Task<BlobDto> UploadAsync(IFormFile file, string filename);

        Task<BlobDto> DownloadAsync(string blobFilename);

        Task<BlobClient> DeleteAsync(string blobFilename);
    }
}
