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
            public Type Type;
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
                Type = typeof(TInterface),
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
                Type = typeof(TInstance),
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
                Type = typeof(TInterface),
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
                Type = typeof(TInterface),
                Instance = new TInstance()
            });

        }

        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static TInstance RegisterSingleton<TInterface, TInstance>(TInstance instance) where TInstance : class
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                Type = typeof(TInterface),
                Instance = instance
            });

            return instance;
        }


        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static TInstance RegisterSingleton<TInstance>(TInstance instance) where TInstance : class
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                Type = instance.GetType(),
                Instance = instance
            });

            return instance;
        }
        /// <summary>
        /// Registers a singleton / shared instance
        /// </summary>
        public static TInstance RegisterSingleton<TInstance>(TInstance instance, Type type) where TInstance : class
        {
            //Todo : Sanity
            Container.Add(new InjectReference
            {
                Type = type,
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
        /// <param name="interfaceType"></param>
        /// <param name="recurrsive">will load dependencie`s dependency</param>
        public static object Get(Type interfaceType, bool recurrsive = true)
        {
            var match = Container.FirstOrDefault(o => interfaceType.IsAssignableFrom(o.Type));

            if (match != null)
            {
                if (match.Instance != null)
                {
                    //find dependencies in dependency
                    if (recurrsive)
                    {
                        return Inject(match.Instance);
                    }
                    else
                    {
                        return match.Instance;
                    }
                }
                else if (match.Factory != null)
                {
                    //find dependencies in dependency
                    if (recurrsive)
                    {
                        return Inject(match.Factory());
                    }
                    else
                    {
                        return match.Factory();
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Resolves dependencies
        /// </summary>
        /// <param name="instance"></param>
        public static T Inject<T>(T instance) where T : class
        {
            try
            {
                //TODO Reflector Cache
                var allFields = instance
                    .GetType()
                    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(o => o.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0)
                    .ToArray();

                for (int i = 0; i < allFields.Length; i++)
                {
                    try
                    {
                        var field = allFields[i];

                        var type = field.FieldType;

                        field.SetValue(instance, Get(type, false));

                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return instance;

        }
        #endregion
    }
}
