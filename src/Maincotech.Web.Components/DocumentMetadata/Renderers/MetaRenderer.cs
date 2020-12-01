using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Html;

namespace Maincotech.Web.Components.DocumentMetadata.Renderers
{
     readonly partial struct Renderer
    {
        public static Renderer Meta(string name, string content)
        {
            return new Renderer(RendererFlag.Meta | RendererFlag.UniqueByName, content, name);
        }

        public int MetaRender(RenderTreeBuilder renderTreeBuilder, int seq)
        {
            renderTreeBuilder.OpenElement(seq + 0, "meta");
            renderTreeBuilder.AddAttribute(seq + 1, "name", _name);
            renderTreeBuilder.AddAttribute(seq + 2, "content", _mainAttributeValue);
            renderTreeBuilder.CloseElement();
            return seq + 3;
        }

        public void MetaRender(IHtmlContentBuilder htmlContentBuilder)
        {
            htmlContentBuilder.AppendHtml($"<meta name='{_name}' content='{_mainAttributeValue}'>");
        }
    }
}