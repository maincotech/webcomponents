using Maincotech.Logging;
using Maincotech.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Maincotech.Web.Components.JQuery
{
    public class JQueryService : IJQueryService
    {
        private static readonly ILogger _Logger = AppRuntimeContext.Current.GetLogger<JQueryService>();
        private IBlobStorage _blogStorage;
        private string _uploadFolder;
        private string _thumbnailFolder;
        private bool _generateThumbnail;

        public JQueryService(IBlobStorage storage, string uploadFolder, bool generateThumbnail = false)
        {
            _blogStorage = storage;
            _uploadFolder = uploadFolder;
            _thumbnailFolder = Path.Combine(_uploadFolder, "thumbs");
            _generateThumbnail = generateThumbnail;
        }

        public async Task<UploadResult> Upload(IFormFile file)
        {
            var result = new UploadResult();

            var fileInfo = new FileInfo(file.FileName);
            var targetFileName = $"{DateTime.UtcNow.ToString("yyyyMMHHddss")}{fileInfo.Extension}";
            var targetPath = Path.Combine(_uploadFolder, targetFileName);
            while (await _blogStorage.Exists(new Blob { Identifier = targetPath, Name = targetFileName }))
            {
                targetFileName = $"{DateTime.UtcNow.ToString("yyyyMMHHddss")}{fileInfo.Extension}";
                targetPath = Path.Combine(_uploadFolder, targetFileName);
            }
            var blob = new Blob
            {
                Identifier = targetPath,
                Name = targetFileName,
            };

            blob = await _blogStorage.SaveBlob(blob, file.OpenReadStream());
            result.Name = file.FileName;
            result.Url = blob.Uri;
            result.Size = blob.Size;
            result.ThumbnailUrl = GetThumbnailUrl(file.FileName, blob);
            return result;
        }

        private string GetThumbnailUrl(string fileName, Blob blob)
        {
            var fileInfo = new FileInfo(fileName);
            var fileExtension = fileInfo.Extension;
            if (fileExtension.EndWithIgnoreCase(".jpeg") ||
                fileExtension.EndWithIgnoreCase(".jpg") ||
                fileExtension.EndWithIgnoreCase(".png") ||
                 fileExtension.EndWithIgnoreCase(".gif")) //image files
            {
                if (_generateThumbnail)
                {
                }
                else
                {
                    return blob.Uri;
                }
            }
            return $"_content/Maincotech.Web.Components/Free-file-icons/48px/{fileExtension.Replace(".", "")}.png";
        }
    }
}