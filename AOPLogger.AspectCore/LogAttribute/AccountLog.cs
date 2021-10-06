using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPLogger.AspectCore.Controllers;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace AOPLogger.AspectCore.LogAttribute
{
    public class AccountLog : AbstractInterceptorAttribute
    {
        [FromServiceContext] public ILogger<AccountLog> Logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                var methodName = context.ImplementationMethod.Name;

                Logger.LogInformation($"methodName: {methodName}");

                if (context.Parameters.GetValue(0) is AccountModal value)
                    Logger.LogInformation($"username: {value.username} , password: {value.password}");

                await next(context); // 進入 Service 前會於此處被攔截（如果符合被攔截的規則）...
            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex} , Method: {context.ImplementationMethod.Name}"); // 記錄例外錯誤...
                throw;
            }
        }
    }
}