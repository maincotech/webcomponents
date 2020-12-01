namespace Microsoft.Extensions.DependencyInjection
{
    using Maincotech.Web.Components.DocumentMetadata;
    using System;

    public static partial class DocumentMetadataExtensions
    {
        public static IServiceCollection AddDocumentMetadata(this IServiceCollection services, Action<IServiceProvider, IDocumentMetadataService> options)
        {
            services.AddSingleton<IDocumentMetadataService>(sp =>
            {
                var documentMetadataService = new DocumentMetadataService();
                options?.Invoke(sp, documentMetadataService);
                return documentMetadataService;
            });
            return services;
        }
    }
}