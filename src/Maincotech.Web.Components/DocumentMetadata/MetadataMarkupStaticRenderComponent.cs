using Maincotech.Web.Components.DocumentMetadata.Renderers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;

namespace Maincotech.Web.Components.DocumentMetadata
{
    internal sealed class MetadataMarkupStaticRenderComponent : ComponentBase
    {
        private static readonly MarkupString TabInsideHeadElement = new MarkupString(Environment.NewLine + new string(' ', 4));

        private readonly ISet<Renderer> _renderers;

        [Parameter] public RenderFragment ChildContent { get; set; }

        public MetadataMarkupStaticRenderComponent(ISet<Renderer> renderers)
        {
            _renderers = renderers;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int seq = 0;
            foreach (var renderer in _renderers)
            {
                builder.AddContent(seq + 0, TabInsideHeadElement);
                seq = renderer.Render(builder, seq + 1);
            }
        }
    }
}