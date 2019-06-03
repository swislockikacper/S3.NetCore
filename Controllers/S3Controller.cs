using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3.NetCore.Extensions;
using S3.NetCore.Interfaces;

namespace S3.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IS3Service s3Service;

        public S3Controller(IS3Service s3Service) => this.s3Service = s3Service;

        [HttpPost("CreateBucket/{bucketName}")]
        public async Task<ActionResult> CreateBucket([FromRoute] string bucketName)
        {
            await s3Service.CreateBucket(bucketName);
            return Ok();
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string bucketName)
        {
            await s3Service.UploadFile(file, bucketName);
            return Ok();
        }

        [HttpGet("DownloadFile/{bucketName}/{fileName}")]
        public async Task<IActionResult> DownloadFile([FromRoute] string bucketName, [FromRoute] string fileName)
        {
            return File(await s3Service.DownloadFile(bucketName, fileName), fileName.GetMimeType());
        }
    }
}
