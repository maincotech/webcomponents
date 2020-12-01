using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Maincotech.Web.Components.DocumentMetadata
{
    public sealed class DocumentMetadataTagHelperComponent : TagHelperComponent
    {
        public override int Order => 1;
        private readonly IHtmlHelper _html;

        private readonly IDocumentMetadataBuilder _documentMetadataBuilder;

        public DocumentMetadataTagHelperComponent(IHtmlHelper html, IDocumentMetadataBuilder builder)
        {
            _documentMetadataBuilder = builder;
        }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                var renderers = _documentMetadataBuilder.Build();
                var builder = new HtmlContentBuilder();
                foreach (var render in renderers)
                {
                    render.Render(builder);
                }
                output.PostContent.AppendHtml(builder);
            }
        }
    }
}