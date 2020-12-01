using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maincotech.Web.Components.JQuery
{
    public interface IJQueryService
    {
        Task<UploadResult> Upload(IFormFile file);
    }
}