using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework
{
    /// <summary>
    /// Identifies a service to be injected
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAttribute : Attribute { }

    /// <summary>
    /// Injection container for registering dependencies
    /// </summary>
    public static class InjectController
    {
        #region Container

        class InjectReference
        {
            public Type InterfaceType;
            public object Instance;
            public Func<object> Factory;
        }

        static List<InjectReference> Container = new List<InjectReference>();

        #endregion

        #region Public Api

        /// <summary>
        /// New instance per request
        /// </summary>
        public static void RegisterTransient<TInterface, TInstance>() where TInstance : class, new()
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInterface),
                Factory = () => { return new TInstance(); },
            });
        }

        /// <summary>
        /// New instance per request
        /// </summary>
        public static void RegisterTransient<TInstance>() where TInstance : class, new()
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInstance),
                Factory = () => { return new TInstance(); },
            });
        }

        /// <summary>
        /// New instance per request
        /// </summary>
        public static void RegisterTransient<TInterface, TInstance>(Func<TInstance> factory) where TInstance : class
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInterface),
                Factory = factory as Func<object>,
            });

        }

        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static void RegisterSingleton<TInterface, TInstance>() where TInstance : class, new()
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInterface),
                Instance = new TInstance()
            });

        }

        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static void RegisterSingleton<TInstance>() where TInstance : class, new()
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInstance),
                Instance = new TInstance()
            });

        }

        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static void RegisterSingleton<TInterface, TInstance>(TInstance instance) where TInstance : class
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInterface),
                Instance = instance
            });

        }

        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static TInstance RegisterSingleton<TInstance>(TInstance instance) where TInstance : class
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                InterfaceType = typeof(TInstance),
                Instance = instance
            });

            return instance;
        }


        /// <summary>
        /// Returns an injected instance
        /// </summary>
        public static TInterface Get<TInterface>() where TInterface : class
        {
            var interfaceType = typeof(TInterface);

            return Get(interfaceType) as TInterface;
        }

        /// <summary>
        /// Returns an injected instance
        /// </summary>
        public static object Get(Type interfaceType)
        {
            var match = Container.FirstOrDefault(o => interfaceType.IsAssignableFrom(o.InterfaceType));

            if (match != null)
            {
                if (match.Instance != null)
                {
                    //find dependencies in dependency
                    return Inject(match.Instance);
                }
                else if (match.Factory != null)
                {
                    //find dependencies in dependency
                    return Inject(match.Factory());
                }
            }

            return null;
        }


        /// <summary>
        /// Resolves dependencies
        /// </summary>
        /// <param name="instance"></param>
        public static T Inject<T>(T instance)
        {
            //TODO Reflector Cache
            var allFields = instance
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0)
                .ToArray();

            for (int i = 0;i < allFields.Length;i++)
            {
                try
                {
                    var field = allFields[i];

                    var type = field.FieldType;

                    field.SetValue(instance, Get(type));
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }

            }

            return instance;

        }
        #endregion
    }
}