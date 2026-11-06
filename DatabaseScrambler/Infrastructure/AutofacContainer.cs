using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;

namespace DatabaseScrambler.Infrastructure
{
    public class AutofacContainer : IContainer
    {
        private readonly Autofac.IContainer _innerContainer;
        private ILifetimeScope _scope;

        public AutofacContainer(Action<ContainerBuilder> builderAction)
        {
            var builder = new ContainerBuilder();

            if (builderAction != null)
                builderAction(builder);

            _innerContainer = builder.Build();
            _scope = _innerContainer;
        }

        /// <summary>
        /// Return the list of all instances that implements T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return _scope.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Return the list of all instances that implements type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IEnumerable ResolveAll(Type type)
        {
            var genericType = typeof(IEnumerable<>).MakeGenericType(type);
            return (IEnumerable)_scope.Resolve(genericType);
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// Throws an exception when the type is not found
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance</returns>		
        public object Resolve(Type type)
        {
            return _scope.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _scope.Resolve<T>();
        }

        /// <summary>
        /// Adds an instance to the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void Add<T>(T instance) where T : class
        {
            _scope = _scope.BeginLifetimeScope(builder => builder.RegisterInstance(instance).As<T>());
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance or null when not found</returns>		
        public object TryResolve(Type type)
        {
            return _scope.IsRegistered(type) ? _scope.Resolve(type) : null;
        }

        public Autofac.IContainer InnerContainer
        {
            get { return _innerContainer; }
        }
    }
}