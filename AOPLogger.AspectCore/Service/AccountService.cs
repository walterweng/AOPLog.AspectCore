using AOPLogger.AspectCore.Controllers;
using AOPLogger.AspectCore.Domain;

namespace AOPLogger.AspectCore.Service
{
    public class AccountService : IAccountService
    {
        private readonly ICustomService _customService;
        private bool _accountCheck;

        public AccountService(ICustomService customService)
        {
            _customService = customService;
        }
        public string GetId(AccountModal accountModal)
        {
            _accountCheck = _customService.AccountCheck(accountModal);
            if (_accountCheck)
            {
                return "walter";
            }

            return "no account";
        }
    }
}