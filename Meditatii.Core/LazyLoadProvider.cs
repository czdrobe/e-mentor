using Meditatii.Core.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public class LazyLoadProvider
    {
        /// <summary>
        /// This holds the service factories passed into the derived controller
        /// </summary>
        private Dictionary<Type, Func<ILazyLoadable>> serviceFactoryContainer = null;

        /// <summary>
        /// This holds instances of the services once used
        /// </summary>
        private ConcurrentDictionary<Type, ILazyLoadable> resolvedServices = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoadProvider"/> class.
        /// The base constructor allows injected service factories to be passed in to be instanitated when needed (this is primarily a code cleanliness concern...)
        /// </summary>
        /// <param name="serviceFactories">param array of service factories availabled to the class</param>
        public LazyLoadProvider(params Func<ILazyLoadable>[] serviceFactories)
        {
            ConfigureServiceFactories(serviceFactories);
        }

        /// <summary>
        /// Put the factories into a collection to be called if needed
        /// </summary>
        /// <param name="serviceFactories">The service factories injected into the derived controller</param>
        private void ConfigureServiceFactories(params Func<ILazyLoadable>[] serviceFactories)
        {
            if (serviceFactories.Any())
            {
                serviceFactoryContainer = serviceFactories.ToDictionary(x => x.GetType().GetGenericArguments()[0], y => y);
            }
        }

        /// <summary>
        /// Retrieve a service by type. If it's already been instantiated we'll use that, otherwise the factory will be called
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve - must defive from ILazyLoadable</typeparam>
        /// <returns>The instantiated service</returns>
        public T GetService<T>()
            where T : ILazyLoadable
        {
            var typeSought = typeof(T);

            if (resolvedServices != null)
            {
                if (resolvedServices.ContainsKey(typeSought))
                {
                    return (T)resolvedServices[typeSought];
                }
            }
            else
            {
                resolvedServices = new ConcurrentDictionary<Type, ILazyLoadable>();
            }

            if (serviceFactoryContainer == null)
            {
                throw new NullReferenceException($"Attempted to get a service of type {typeSought} but the serviceFactoryContainer has not been initialised so no services are present on " + this.GetType());
            }

            ILazyLoadable resolvedService = null;

            if (serviceFactoryContainer.ContainsKey(typeSought))
            {
                resolvedService = serviceFactoryContainer[typeSought]();
            }
            else
            {
                try
                {
                    var serviceKey = serviceFactoryContainer.FirstOrDefault(x => typeof(T).IsAssignableFrom(x.Key)).Key;
                    if (serviceKey != null)
                    {
                        resolvedService = serviceFactoryContainer[serviceKey]();
                    }
                }
                catch (Exception ex) // we'll handle the fact
                {
                    Trace.TraceError("Error getting service in service resolver: " + ex);
                }
            }

            if (resolvedService == null)
            {
                throw new NullReferenceException($"Attempted to get a service of type {typeSought} but the serviceFactoryContainer didn't have a reference to that type " + this.GetType());
            }

            resolvedServices.TryAdd(typeSought, resolvedService);
            return (T)resolvedServices[typeSought];
        }
    }
}
