using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Handlers;
using ObjectObfuscator.Handlers;
using System;

namespace ObjectObfuscator.Extensions.DependencyInjection
{
    public static class ObfuscatorInstaller
    {
        public static IServiceCollection AddObjectObfuscator(this IServiceCollection services)
        {
            return services.AddObjectObfuscator(configure =>
            {
                configure.MaxDeepLevel = 10;
            });
        }

        public static IServiceCollection AddObjectObfuscator(this IServiceCollection services, Action<ObfuscatorConfiguration> configure)
        {
            services.TryAddTransient<ClassTypeHandler>();
            services.TryAddTransient<StructTypeHandler>();
            services.TryAddTransient<ArrayTypeHandler>();
            services.TryAddTransient<CollectionTypeHandler>();
            services.TryAddTransient<RemoveValueHandler>();
            services.TryAddTransient<RemoveValueHandler>();
            services.TryAddTransient<ITypeDetectHandler, DictionaryTypeHandler>();

            services.TryAddTransient<IStringValueHandler, StringValueHandler>();
            services.TryAddTransient<IPropertyHandler, ObfuscateValueHandler>();

            var configuration = new ObfuscatorConfiguration();

            configure.Invoke(configuration);

            services.RemoveAll<IObfuscator>();

            services.AddTransient<IObfuscator>(provider =>
            {
                var typeHandler = provider.GetService<ITypeDetectHandler>();
                var propertyHandler = provider.GetService<IPropertyHandler>();
                return new Obfuscator(typeHandler, propertyHandler, configuration.MaxDeepLevel);
            });

            return services;
        }
    }
}