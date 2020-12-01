using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Maincotech.Web.Components
{
    public class MaincotechJsInterop
    {
        public static ValueTask<string> Prompt(IJSRuntime jsRuntime, string message)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }

        /// <summary>
        /// === Generate a bin file===
        ///  byte[] file = Enumerable.Range(0, 100).Cast<byte>().ToArray();
        ///  await MaincotechJsInterop.SaveFileAsync(jsRuntime, "file.bin", "application/octet-stream", file);
        /// === Generate a bin file===
        /// === Generate a text file===
        ///  byte[] file = System.Text.Encoding.UTF8.GetBytes("Hello world!");
        ///  await MaincotechJsInterop.SaveFileAsync(jsRuntime, "file.txt", "text/plain", file);
        /// === Generate a text file===
        ///
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ValueTask SaveFileAsync(IJSRuntime jsRuntime, string fileName, string contentType, byte[] content)
        {
            return jsRuntime.InvokeVoidAsync("Maincotech.SaveFile", fileName, contentType, content);
        }
    }
}