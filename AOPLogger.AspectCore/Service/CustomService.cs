using AOPLogger.AspectCore.Controllers;
using AOPLogger.AspectCore.Domain;

namespace AOPLogger.AspectCore.Service
{
    public class CustomService: ICustomService
    {
        public bool AccountCheck(AccountModal accountModal)
        {
            return true;
        }

       public bool PassworkCheck(AccountModal accountModal)
       {
           return true;
       }
    }
}