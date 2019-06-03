using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult CreateBucket([FromRoute] string bucketName)
        {
            s3Service.CreateBucket(bucketName);
            return Ok();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
