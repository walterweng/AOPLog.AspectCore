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
        private readonly IAccountService _accountService;
        public ICustomService _customService { get; }

        public HomeController(ICustomService customService , IAccountService accountService)
        {
            _accountService = accountService;
            _customService = customService;
        }

        [HttpGet]
        public string Index()
        {
            AccountModal accountModal = new AccountModal
            {
                username = "walter.weng",
                password = "111"
            };
            


            return _accountService.GetId(accountModal);
        }
    }

    public class AccountModal
    {
        public string username { get; set; }
        public string password { get; set; }

    }
}
