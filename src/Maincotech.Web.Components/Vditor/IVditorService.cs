using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maincotech.Web.Components.Vditor
{
    public interface IVditorService
    {
        Task<UploadResult> Upload(IEnumerable<IFormFile> files);

        Task<LinkToImgUrlResult> Convert(LinkToImgUrlRequest request);
    }
}