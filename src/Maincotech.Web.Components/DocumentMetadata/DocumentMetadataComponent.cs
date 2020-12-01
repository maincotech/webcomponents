using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Maincotech.Web.Components.DocumentMetadata
{
    public class DocumentMetadataComponent : ComponentBase
    {
        private static readonly MarkupString TabInsideHeadElement = new MarkupString(Environment.NewLine + new string(' ', 4));

        [Inject] public IDocumentMetadataService DocumentMetadataService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            NavigationManager.LocationChanged += (sender, args) => InvokeAsync(StateHasChanged);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int seq = 0;
            var renderers = DocumentMetadataService.GetRenderers(NavigationManager.GetCurrentPageName());
            if (renderers != null)
            {
                string tilteFormat = "{0}";
                foreach (var renderer in renderers)
                {
                    builder.AddContent(seq + 0, TabInsideHeadElement);
                    if (renderer.IsTitleFormat)
                    {
                        tilteFormat = renderer.Value;
                    }

                    seq = renderer.Render(builder, seq + 1, tilteFormat);
                }
            }
        }
    }

    internal static class NavigationManagerExtensons
    {
        public static string GetCurrentPageName(this NavigationManager navigationManager) => navigationManager.GetPageNameByLocation(navigationManager.Uri);

        public static string GetPageNameByLocation(this NavigationManager navigationManager, string location)
        {
            var uriFragment = navigationManager.ToAbsoluteUri(location).Fragment;
            if (!string.IsNullOrEmpty(uriFragment))
                location = location.Replace(uriFragment, "");
            return navigationManager.ToBaseRelativePath(location);
        }

        public static string ResolveUrl(this NavigationManager navigationManager, string url)
        {
            if (url.StartsWith("~/"))
            {
                string baseUrl = navigationManager.BaseUri;
                string absoluteUrl = baseUrl + url.Substring(2);
                url = navigationManager.ToBaseRelativePath(absoluteUrl);
                url = navigationManager.ToAbsoluteUri(url).PathAndQuery;
            }
            return url;
        }
    }
}