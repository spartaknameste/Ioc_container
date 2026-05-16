using Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoC_Containers
{
    internal class Ioc_Container
    {
        private readonly Dictionary<Type, Mark> _registrations = new();

        public void AddSingleton<TInterface, TImplementation>()
            where TImplementation : class, TInterface
        {
            _registrations[typeof(TInterface)] = new Mark
            {
                ImplementationType = typeof(TImplementation),
                Lifetime = Lifetime.Singleton
            };
        }

        public void AddTransient<TInterface, TImplementation>()
            where TImplementation : class, TInterface
        {
            _registrations[typeof(TInterface)] = new Mark
            {
                ImplementationType = typeof(TImplementation),
                Lifetime = Lifetime.Transient
            };
        }

        public TService Resolve<TService>() where TService : class
        {
            return (TService)Resolve(typeof(TService));
        }
        private object Resolve(Type serviceType)
        {
            if (_registrations.ContainsKey(serviceType))
            {
                var registration = _registrations[serviceType];
                if (registration.Lifetime == Lifetime.Singleton)
                {
                    if (registration.SingletonInstance == null)
                    {
                        registration.SingletonInstance = CreateInstance(registration.ImplementationType);
                    }
                    return registration.SingletonInstance;
                }
                return CreateInstance(registration.ImplementationType);
            }

            if (!serviceType.IsAbstract && !serviceType.IsInterface)
            {
                return CreateInstance(serviceType);
            }

            throw new InvalidOperationException(
                $"Тип {serviceType.Name} не зарегистрирован в контейнере.");
        }

        private object CreateInstance(Type typeServ)
        {
            var constructor = typeServ.GetConstructors().First();
            var parameters = constructor.GetParameters();

            var dependencies = parameters
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return constructor.Invoke(dependencies);
        }
    }
}