using System;

namespace Maincotech.Blazor
{
    public interface ILayoutProvider
    {
        Type GetLayoutForPage(string pageTypeFullName);
        void Register(string pageTypeFullName, Type layoutType);
    }
}