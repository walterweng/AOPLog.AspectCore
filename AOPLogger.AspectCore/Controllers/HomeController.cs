using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPLogger.AspectCore.Domain;

namespace AOPLogger.AspectCore.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class HomeController : Controller
    {
        public ICustomService _customService { get; }

        public HomeController(ICustomService customService)
        {
            _customService = customService;
        }

        [HttpGet]
        public bool Index()
        {
            AccountModal accountModal = new AccountModal
            {
                username = "walter.weng",
                password = "111"
            };
            
            var accountCheck = _customService.AccountCheck(accountModal);
            var passworkCheck = _customService.PassworkCheck(accountModal);

            return accountCheck;
        }
    }

    public class AccountModal
    {
        public string username { get; set; }
        public string password { get; set; }

    }
}
