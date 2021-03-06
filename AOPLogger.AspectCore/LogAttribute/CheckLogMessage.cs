using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPLogger.AspectCore.Controllers;
using AOPLogger.AspectCore.Domain;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AOPLogger.AspectCore.LogAttribute
{
    public class CheckLogMessage : AbstractInterceptorAttribute
    {
        private readonly IAccountService _accountService;

        public CheckLogMessage(IAccountService _accountService)
        {
            this._accountService = _accountService;
        }

        [FromServiceContext] public ILogger<AccountLogMessage> Logger { get; set; }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {

            var className = context.ImplementationMethod.DeclaringType.FullName.Split(".").LastOrDefault();
            var methodName = context.ImplementationMethod.Name;

            var Name = context.Implementation.ToString();

            var implementationMethodReturnType = context.ImplementationMethod.ReturnType;
            var implementationMethodDeclaringType = context.ImplementationMethod.DeclaringType;

            try
            {

                Console.WriteLine($"{className}.{methodName} -- Start");

                await next(context); // 進入 Service 前會於此處被攔截（如果符合被攔截的規則）...

                Console.WriteLine($"{className}.{methodName} -- End");

            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex} , Method: {methodName}"); // 記錄例外錯誤...
                throw;
            }
        }
    }
}
