using System.Collections.Generic;

namespace Maincotech.Web.Components.Vditor
{
    public class UploadOptions
    {
        public string Url { get; set; }

        //Size in Bytes
        public int Max { get; set; } = 10 * 1024 * 1024;

        public string Accept { get; set; } = "image/*,.mp3, .wav, .rar";

        public string LinkToImgUrl { get; set; }
    }

    public class UploadResult
    {
        public string Message { get; set; }

        public int Code { get; set; }

        public ResultData Data { get; set; }

        public class ResultData
        {
            public List<string> ErrFiles { get; set; }
            public Dictionary<string, string> SuccMap { get; set; }
        }
    }

    public class LinkToImgUrlResult
    {
        public string Message { get; set; }

        public int Code { get; set; }

        public ResultData Data { get; set; }

        public class ResultData
        {
            public string OriginalURL { get; set; }
            public string Url { get; set; }
        }
    }

    public class LinkToImgUrlRequest
    {
        public string Url { get; set; }
    }
}