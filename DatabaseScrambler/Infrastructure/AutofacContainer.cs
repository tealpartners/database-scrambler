using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;

namespace DatabaseScrambler.Infrastructure
{
    public class AutofacContainer : IContainer
    {
        private readonly Autofac.IContainer _innerContainer;

        public AutofacContainer(Action<ContainerBuilder> builderAction)
        {
            var builder = new ContainerBuilder();

            if (builderAction != null)
                builderAction(builder);

            _innerContainer = builder.Build();
        }

        /// <summary>
        /// Return the list of all instances that implements T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return _innerContainer.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Return the list of all instances that implements type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IEnumerable ResolveAll(Type type)
        {
            var genericType = typeof(IEnumerable<>).MakeGenericType(type);
            return (IEnumerable)_innerContainer.Resolve(genericType);
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// Throws an exception when the type is not found
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance</returns>		
        public object Resolve(Type type)
        {
            return _innerContainer.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _innerContainer.Resolve<T>();
        }

        /// <summary>
        /// Adds an instance to the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void Add<T>(T instance) where T : class
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(instance)
                .As<T>();

            builder.Update(_innerContainer);
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance or null when not found</returns>		
        public object TryResolve(Type type)
        {
            return _innerContainer.IsRegistered(type) ? _innerContainer.Resolve(type) : null;
        }

        public Autofac.IContainer InnerContainer
        {
            get { return _innerContainer; }
        }
    }
}