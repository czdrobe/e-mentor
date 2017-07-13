using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Meditatii.Core.Helpers
{
    public static class MappingHelper
    {
        /// <summary>
        /// Map between source and target types explicitly. Adds service constructor.
        /// </summary>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TTarget">Target object type</typeparam>
        /// <param name="source">The object to transform from</param>
        /// <returns>Target typed object</returns>
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            return Map<TTarget>(source);
        }

        /// <summary>
        /// Map between source and target types using source derived from object type. Adds service constructor.
        /// </summary>
        /// <typeparam name="TTarget">Target object type</typeparam>
        /// <param name="source">The object to transform from</param>
        /// <returns>Target typed object</returns>
        public static TTarget Map<TTarget>(object source)
        {
            if (HttpContext.Current != null)
            {
                return Mapper.Map<TTarget>(source, opt => opt.ConstructServicesUsing(DependencyResolver.Current.GetService));
            }
            else
            {
                return Mapper.Map<TTarget>(source);
            }
        }

        public static object Map<TTarget>(object source, bool usingInterface)
        {
            if (usingInterface)
            {
                try
                {
                    var type = typeof(TTarget);
                    if (type.IsInterface)
                    {
                        var entityResolved = DependencyResolver.Current.GetService(type);

                        var result = Mapper.Map(source, entityResolved, source.GetType(), entityResolved.GetType(), opt => opt.ConstructServicesUsing(DependencyResolver.Current.GetService));

                        return result;
                    }
                    else
                    {
                        if (type.IsGenericType && type.GetGenericTypeDefinition().GetInterface("IEnumerable") != null)
                        {
                            type = type.GetGenericArguments()[0];

                            var entityResolved = DependencyResolver.Current.GetService(type);
                            var listType = typeof(List<>);
                            var constructedListType = listType.MakeGenericType(entityResolved.GetType());

                            var listInstance = Activator.CreateInstance(constructedListType);

                            var result = Mapper.Map(source, listInstance, source.GetType(), listInstance.GetType(), opt => opt.ConstructServicesUsing(DependencyResolver.Current.GetService));

                            return (IEnumerable)result;
                        }
                        else
                        {
                            throw new Exception("If usingInterface is set to True, the generic paramenter has to be an interface or a list of interfaces");
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message);
                    throw;
                }
            }
            else
            {
                return Map<TTarget>(source);
            }
        }
    }
}
