using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Html;

namespace Maincotech.Web.Components.DocumentMetadata.Renderers
{
    internal readonly partial struct Renderer
    {
        public static Renderer BaseHref(string url)
        {
            return new Renderer(RendererFlag.BaseHref | RendererFlag.UniqueByType, url);
        }

        public int BaseHrefRender(RenderTreeBuilder renderTreeBuilder, int seq)
        {
            renderTreeBuilder.OpenElement(seq + 0, "base");
            renderTreeBuilder.AddAttribute(seq + 1, "href", _mainAttributeValue);
            renderTreeBuilder.CloseElement();
            return seq + 2;
        }

        public void BaseHrefRender(IHtmlContentBuilder htmlContentBuilder)
        {
            htmlContentBuilder.AppendHtml($"<base href='{_mainAttributeValue}'>");
        }
    }
}