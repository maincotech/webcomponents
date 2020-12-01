using System;
using System.Collections.Concurrent;

namespace Maincotech.Blazor
{
    public class DefaultLayoutProvider : ILayoutProvider
    {

        private readonly Type _defaultLayout;
        private ConcurrentDictionary<string, Type> _pageLayouts = new ConcurrentDictionary<string, Type>();

        public DefaultLayoutProvider(Type defaultLayout)
        {
            _defaultLayout = defaultLayout;
        }

        public Type GetLayoutForPage(string pageTypeFullName)
        {
            if (_pageLayouts.ContainsKey(pageTypeFullName))
            {
                return _pageLayouts[pageTypeFullName];
            }
            return _defaultLayout;
        }

        public void Register(string pageTypeFullName, Type layoutType)
        {
            _pageLayouts.TryAdd(pageTypeFullName, layoutType);
        }
    }
}