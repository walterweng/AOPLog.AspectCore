using AOPLogger.AspectCore.Controllers;

namespace AOPLogger.AspectCore.Domain
{
    public interface IAccountService
    {
        string GetId(AccountModal accountModal);
    }
}