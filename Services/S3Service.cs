using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using S3.NetCore.Interfaces;

namespace S3.NetCore.Services
{
    public class S3Service : IS3Service
    {
        private const string accessKeyId = "PLACEHOLDER";
        private const string accessKey = "PLACEHOLDER";

        public async Task CreateBucket(string bucketName)
        {
            using (var client = new AmazonS3Client(accessKeyId, accessKey, RegionEndpoint.USEast2))
            {
                if (await AmazonS3Util.DoesS3BucketExistAsync(client, bucketName) == false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    await client.PutBucketAsync(putBucketRequest);
                }
            }
        }

        public async Task UploadFile(IFormFile file, string bucketName)
        {
            using (var client = new AmazonS3Client(accessKeyId, accessKey, RegionEndpoint.USEast2))
            {
                var fileTransferUtility = new TransferUtility(client);

                using (var fileToUpload = file.OpenReadStream())
                {
                    await fileTransferUtility.UploadAsync(fileToUpload, bucketName, file.FileName);
                }
            }
        }

        public async Task<byte[]> DownloadFile(string bucketName, string fileName)
        {
            using (var client = new AmazonS3Client(accessKeyId, accessKey, RegionEndpoint.USEast2))
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                using (var response = await client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)
                {
                    var memoryStream = new MemoryStream();
                    await responseStream.CopyToAsync(memoryStream);

                    memoryStream.Position = 0;

                    return memoryStream.ToArray();
                }
            }
        }
    }
}