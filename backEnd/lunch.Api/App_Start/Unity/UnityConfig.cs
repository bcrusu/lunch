using System;
using lunch.BusinessLogic;
using lunch.Repositories;
using Microsoft.Practices.Unity;

namespace lunch.Api.Unity
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterRepositoryTypes();
            container.RegisterBusinessLogicTypes();
        }
    }
}
