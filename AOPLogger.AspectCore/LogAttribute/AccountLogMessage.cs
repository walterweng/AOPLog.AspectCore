using System;
using System.Linq;
using System.Threading.Tasks;
using AOPLogger.AspectCore.Domain;
using AOPLogger.AspectCore.Service;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;


namespace AOPLogger.AspectCore.LogAttribute
{
    public class AccountLogMessage : AbstractInterceptorAttribute
    {
        private readonly INLogWriteService _logWriteService;

        [FromServiceContext]
        string MatchId = "RB1511111";
        long sequence = 200;
        string code = "E";
        string cid = "122151xasda4415qwe4";
        public AccountLogMessage(INLogWriteService logWriteService)
        {
            _logWriteService = logWriteService;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                
                var className = context.ImplementationMethod.DeclaringType.FullName.Split(".").LastOrDefault();
                var methodName = context.ImplementationMethod.Name;

                Console.WriteLine($"{className}.{methodName} -- Start");

                string message = "This is an error message!";

                _logWriteService.EventInfoLog(MatchId, sequence, $"{className}.{methodName}", message);
                _logWriteService.MatchInfoLog(MatchId, $"{className}.{methodName}", message);
                _logWriteService.StateInfoLog(MatchId, sequence, $"{className}.{methodName}", message);

                await next(context); // 進入 Service 前會於此處被攔截（如果符合被攔截的規則）...
                Console.WriteLine($"{className}.{methodName} ReturnValue : {fastJSON.JSON.ToJSON(context.ReturnValue)}");
                Console.WriteLine($"{className}.{methodName} -- End");
            }
            catch (Exception ex)
            {
                var className = context.ImplementationMethod.DeclaringType.FullName.Split(".").LastOrDefault();
                var methodName = context.ImplementationMethod.Name;

                string message = ex.ToString();

                _logWriteService.EventErrorLog(MatchId, sequence, $"{className}.{methodName}", message);
                _logWriteService.MatchErrorLog(MatchId, $"{className}.{methodName}", message);
                _logWriteService.StateErrorLog(MatchId, sequence, $"{className}.{methodName}", message);
            }
        }
    }
}