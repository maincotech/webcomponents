using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Html;

namespace Maincotech.Web.Components.DocumentMetadata.Renderers
{
    internal readonly partial struct Renderer
    {
        public static Renderer Script(string url, bool async, bool defer)
        {
            var optionalFlags = 1;
            if (async)
                optionalFlags = 1 << 2;
            if (defer)
                optionalFlags = 1 << 3;
            return new Renderer(RendererFlag.Script | RendererFlag.UniqueByName, url, null, optionalFlags);
        }

        public int ScriptRender(RenderTreeBuilder renderTreeBuilder, int seq)
        {
            bool
                async = _optionalAttributes >> 2 == 1,
                defer = _optionalAttributes >> 3 == 1;

            renderTreeBuilder.OpenElement(seq + 0, "script");
            renderTreeBuilder.AddAttribute(seq + 1, "src", _mainAttributeValue);
            if (async)
                renderTreeBuilder.AddAttribute(seq + 2, "async", true);
            else if (defer)
                renderTreeBuilder.AddAttribute(seq + 2, "defer", true);
            renderTreeBuilder.CloseElement();
            return seq + 4;
        }

        public void ScriptRender(IHtmlContentBuilder htmlContentBuilder)
        {
            bool
                async = _optionalAttributes >> 2 == 1,
                defer = _optionalAttributes >> 3 == 1;

            if (async)
            {
                htmlContentBuilder.AppendHtml($"<script src='{_mainAttributeValue}' async></script>");
            }
            else if (defer)
            {
                htmlContentBuilder.AppendHtml($"<script src='{_mainAttributeValue}' defer></script>");
            }
            else
            {
                htmlContentBuilder.AppendHtml($"<script src='{_mainAttributeValue}'></script>");
            }
        }
    }
}