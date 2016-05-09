using lunch.BusinessLogic.Impl.Security;
using lunch.BusinessLogic.Security;
using Microsoft.Practices.Unity;

namespace lunch.BusinessLogic
{
    public static class UnityConfig
    {
        public static void RegisterBusinessLogicTypes(this IUnityContainer container)
        {
            container.RegisterType<IExternalUsersBusinessLogic, ExternalUsersBusinessLogic>();
        }
    }
}
