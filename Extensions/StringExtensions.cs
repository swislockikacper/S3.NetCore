using System.IO;

namespace S3.NetCore.Extensions
{
    public static class StringExtensions
    {
        public static string GetMimeType(this string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".txt":
                    return "text/plain";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                    return "application/vnd.ms-word";
                case ".docx":
                    return "application/vnd.ms-word";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpeg";
                case ".jpeg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                case ".csv":
                    return "text/csv";
                default:
                    return string.Empty;
            }
        }
    }
}