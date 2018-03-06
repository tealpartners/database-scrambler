﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DatabaseScrambler.Infrastructure
{
    public interface IContainer
    {
        /// <summary>
        /// Return the list of all instances that implements T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Return the list of all instances that implements type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IEnumerable ResolveAll(Type type);

        /// <summary>
        /// Resolve an instance of the specified type
        /// Throws an exception when the type is not found
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance</returns>		
        Object Resolve(Type type);

        /// <summary>
        /// Resolve an instance of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Adds an instance to the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void Add<T>(T instance) where T : class;

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>		
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance or null when not found</returns>		
        object TryResolve(Type type);
    }
}