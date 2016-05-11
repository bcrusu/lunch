using System;
using lunch.BusinessLogic;
using lunch.Repositories;
using Microsoft.Practices.Unity;

namespace lunch.Api.Unity
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(CreateUnityContainer);

        public static IUnityContainer GetContainer()
        {
            return Container.Value;
        }

        private static IUnityContainer CreateUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterRepositoryTypes();
            container.RegisterBusinessLogicTypes();
        }

        public static void DisposeContainer()
        {
            if (!Container.IsValueCreated)
                return;

            Container.Value.Dispose();
        }
    }
}
