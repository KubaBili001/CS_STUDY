using EFCore_Prescriptions.Logger;

namespace EFCore_Prescriptions.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                LogWriter.WriteLog(ex.ToString());

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected problem occured -> log.txt");
            }
        }

    }
}
