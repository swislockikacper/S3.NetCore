using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace S3.NetCore.Interfaces
{
    public interface IS3Service
    {
        Task CreateBucket(string bucketName);
        Task UploadFile(IFormFile file, string bucketName);
        Task<byte[]> DownloadFile(string bucketName, string fileName);
    }
}