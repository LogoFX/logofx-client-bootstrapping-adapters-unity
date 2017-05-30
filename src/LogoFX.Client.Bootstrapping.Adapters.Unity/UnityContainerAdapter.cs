﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LogoFX.Client.Bootstrapping.Adapters.Contracts;
using Microsoft.Practices.Unity;
using Solid.Practices.IoC;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity
{
    /// <summary>
    /// Represents implementation of IoC container and bootstrapper adapter using Unity Container.
    /// </summary>
    public class UnityContainerAdapter : IIocContainer, IIocContainerAdapter<UnityContainer>, IBootstrapperAdapter
    {
        private readonly UnityContainer _container = new UnityContainer();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContainerAdapter"/> class.
        /// </summary>
        public UnityContainerAdapter()
        {
            _container.RegisterInstance(_container);
            _container.RegisterType(typeof(IEnumerable<>), 
                new InjectionFactory((container, type, name) => 
                container.ResolveAll(type.GetTypeInfo().GenericTypeArguments.Single())));
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        public void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>();
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        public void RegisterTransient<TService>() where TService : class
        {
            _container.RegisterType<TService>();            
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>
        public void RegisterTransient(Type serviceType, Type implementationType)
        {
            _container.RegisterType(serviceType, implementationType);
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterTransient<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterType<TService>(new InjectionFactory(context => dependencyCreator()));
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterTransient<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterType<TService>(new InjectionFactory(context => dependencyCreator()));
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterType(serviceType, implementationType,
                new InjectionFactory(context => dependencyCreator()));
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        public void RegisterSingleton<TService>() where TService : class
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>        
        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            _container.RegisterType(serviceType, implementationType, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterSingleton<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(context => dependencyCreator()));
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterSingleton<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager(), new InjectionFactory(context => dependencyCreator()));
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterType(serviceType, implementationType, new ContainerControlledLifetimeManager(),
                new InjectionFactory(context => dependencyCreator()));
        }

        /// <summary>
        /// Registers an instance of dependency.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        /// <param name="instance">Instance of dependency.</param>
        public void RegisterInstance<TService>(TService instance) where TService : class
        {
            _container.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="dependencyType">Type of the dependency.</param>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RegisterInstance(Type dependencyType, object instance)
        {
            _container.RegisterInstance(dependencyType, instance, new ContainerControlledLifetimeManager());
        }        

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="dependencyTypes">The dependency types.</param>
        public void RegisterCollection<TService>(IEnumerable<Type> dependencyTypes) where TService : class
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterType(typeof (TService), type, type.Name);
            }            
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="dependencies">The dependencies.</param>
        public void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class
        {
            _container.RegisterInstance(dependencies);
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencyTypes">The dependency types.</param>
        public void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes)
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterType(dependencyType, type, type.Name);
            }            
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencies">The dependencies.</param>
        public void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies)
        {
            foreach (var dependency in dependencies)
            {
                _container.RegisterType(dependencyType, dependencyType, dependency.GetType().FullName);
            }                       
        }

        /// <summary>
        /// Resolves an instance of service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns/>
        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }

        /// <summary>
        /// Resolves an instance of service according to the service type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns/>
        public object Resolve(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        /// <summary>
        /// Resolves an instance of required service by its type.
        /// </summary>
        /// <param name="serviceType">Type of service.</param>
        /// <param name="key">Optional service key.</param>
        /// <returns>Instance of service.</returns>
        public object GetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType);
        }

        /// <summary>
        /// Resolves all instances of required service by its type.
        /// </summary>
        /// <param name="serviceType">Type of service.</param>
        /// <returns>All instances of requested service.</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        /// <summary>
        /// Resolves instance's dependencies and injects them into the instance.
        /// </summary>
        /// <param name="instance">Instance to get injected with dependencies.</param>
        public void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>        
        public void Dispose()
        {
            ((IDisposable) _container).Dispose();
        }
    }
}
