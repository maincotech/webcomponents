using Maincotech.Web.Components.DocumentMetadata.Renderers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Maincotech.Web.Components.DocumentMetadata
{
    public interface IDocumentMetadataService
    {
        IDocumentMetadataBuilder AddDefault();

        IDocumentMetadataBuilder AddPage(string pageName);

        internal IEnumerable<Renderer> GetRenderers(string pageName);
    }

    internal sealed class MetadataCache : ConcurrentDictionary<string, ISet<Renderer>>
    {
        internal ISet<Renderer> GetPageRenderers(string pageName)
        {
            return GetOrAdd(GetFixedPageName(pageName), (_) => new MetadataRendererSet());
        }

        internal bool TryGetPageRenderers(string pageName, out ISet<Renderer> _) => TryGetValue(GetFixedPageName(pageName), out _);

        private static string GetFixedPageName(string route)
        {
            if (route.Length == 0 || route[0] != '/')
                return route.Insert(0, "/");
            else
                return route;
        }
    }

    internal sealed class DocumentMetadataService : IDocumentMetadataService, IDisposable
    {
        private static readonly string
            DefaultMetadataCacheKey = $"default_{Guid.NewGuid()}",
            OverrideMetadataCacheKey = $"override_{Guid.NewGuid()}",
            RclBaseAssemblyName = typeof(ComponentBase).Assembly.GetName().Name;

        private readonly MetadataCache _cache = new MetadataCache();

        public IDocumentMetadataBuilder AddDefault() => AddPage(DefaultMetadataCacheKey);

        public IDocumentMetadataBuilder AddPage(string pageName) => new DocumentMetadataBuilder(pageName, _cache.GetPageRenderers(pageName));

        private IEnumerable<Renderer> MergeRenderers(
                IEnumerable<Renderer> prev,
                IEnumerable<Renderer> next) =>
                    prev.Except(next, MetadataRendererComparer.Default).
                    Concat(next.Except(prev, MetadataRendererComparer.Default));

        private static IEnumerable<IEnumerable<Renderer>> GetRenderers(MetadataCache cache, params string[] keys) => keys.Select(x => GetRenderers(cache, x));

        private static IEnumerable<Renderer> GetRenderers(MetadataCache cache, string key) =>
            cache.TryGetPageRenderers(key, out ISet<Renderer> renderers) ? renderers : Enumerable.Empty<Renderer>();

        void IDisposable.Dispose() => _cache.Clear();

        IEnumerable<Renderer> IDocumentMetadataService.GetRenderers(string pageName) =>
            GetRenderers(_cache, DefaultMetadataCacheKey, pageName, OverrideMetadataCacheKey).
            Where(Enumerable.Any).
            Aggregate(MergeRenderers);
    }
}