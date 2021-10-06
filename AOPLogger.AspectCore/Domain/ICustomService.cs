using AOPLogger.AspectCore.Controllers;
using AspectCore.DynamicProxy;

namespace AOPLogger.AspectCore.Domain
{
    public interface ICustomService
    {
        bool AccountCheck(AccountModal accountModal);

        [NonAspect]
        bool PassworkCheck(AccountModal accountModal);
    }
}