using Maincotech.Web.Components.DocumentMetadata.Renderers;
using System.Collections.Generic;

namespace Maincotech.Web.Components.DocumentMetadata
{
    internal sealed class MetadataRendererSet : HashSet<Renderer>
    {
        internal MetadataRendererSet() : base(MetadataRendererComparer.Default)
        {
        }
    }

    internal sealed class MetadataRendererComparer : IEqualityComparer<Renderer>
    {
        public bool Equals(Renderer x, Renderer y) => x.Equals(y);

        public int GetHashCode(Renderer obj) => obj.GetHashCode();

        public static readonly IEqualityComparer<Renderer> Default = new MetadataRendererComparer();

        private MetadataRendererComparer()
        { }
    }
}