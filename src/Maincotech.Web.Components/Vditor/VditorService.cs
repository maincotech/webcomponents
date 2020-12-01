using Maincotech.Logging;
using Maincotech.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Maincotech.Web.Components.Vditor
{
    public class VditorService : IVditorService
    {
        private static readonly ILogger _Logger = AppRuntimeContext.Current.GetLogger<VditorService>();
        private IBlobStorage _blogStorage;
        private string _uploadFolder;

        public VditorService(IBlobStorage storage, string uploadFolder)
        {
            _blogStorage = storage;
            _uploadFolder = uploadFolder;
        }

        public Task<LinkToImgUrlResult> Convert(LinkToImgUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UploadResult> Upload(IEnumerable<IFormFile> files)
        {
            var result = new UploadResult
            {
                Code = 0,
                Message = "Uploaded",
                Data = new UploadResult.ResultData
                {
                    ErrFiles = new List<string>(),
                    SuccMap = new Dictionary<string, string>()
                }
            };
            foreach (var file in files)
            {
                try
                {
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
                    result.Data.SuccMap.Add(file.FileName, blob.Uri);
                }
                catch (Exception ex)
                {
                    _Logger.Error($"Failed to upload file :{file.FileName}", ex);
                    result.Data.ErrFiles.Add(file.FileName);
                }
            }

            return result;
        }
    }
}