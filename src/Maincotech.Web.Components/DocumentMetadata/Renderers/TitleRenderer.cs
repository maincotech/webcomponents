using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Html;

namespace Maincotech.Web.Components.DocumentMetadata.Renderers
{
    internal readonly partial struct Renderer
    {
        public static Renderer Title(string value)
        {
            return new Renderer(RendererFlag.Title | RendererFlag.UniqueByType, value);
        }

        public static Renderer TitleFormat(string value)
        {
            return new Renderer(RendererFlag.TitleFormat | RendererFlag.UniqueByType, value);
        }

        public int TitleRender(RenderTreeBuilder renderTreeBuilder, int seq,string titleFormat)
        {
            renderTreeBuilder.OpenElement(seq + 0, "title");
            renderTreeBuilder.AddContent(seq + 1, string.Format(titleFormat, _mainAttributeValue));
            renderTreeBuilder.CloseElement();
            return seq + 2;
        }

        public void TitleRender(IHtmlContentBuilder htmlContentBuilder)
        {
            htmlContentBuilder.AppendHtml($"<title>{_mainAttributeValue}</title>");
        }

        public int TitleFormatRender(RenderTreeBuilder renderTreeBuilder, int seq)
        {
            return seq;
        }
    }
}