using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Html;

namespace Maincotech.Web.Components.DocumentMetadata.Renderers
{
     readonly partial struct Renderer
    {
        public static Renderer Charset(string value)
        {
            return new Renderer(RendererFlag.Charset | RendererFlag.UniqueByType, value);
        }

        public int CharsetRender(RenderTreeBuilder renderTreeBuilder, int seq)
        {
            renderTreeBuilder.OpenElement(seq + 0, "meta");
            renderTreeBuilder.AddAttribute(seq + 1, "charset", _mainAttributeValue);
            renderTreeBuilder.CloseElement();
            return seq + 2;
        }

        public void CharsetRender(IHtmlContentBuilder builder)
        {
            builder.AppendHtml($"<meta charset='{_mainAttributeValue}'>");
        }
    }
}