namespace Maincotech.Web.Components.DocumentMetadata.Renderers
{
    internal readonly partial struct Renderer
    {
        public static Renderer Viewport(string value)
        {
            return Meta("viewport", value);
        }
    }
}