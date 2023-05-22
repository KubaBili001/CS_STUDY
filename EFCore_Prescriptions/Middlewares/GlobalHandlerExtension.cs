using System.Runtime.CompilerServices;

namespace EFCore_Prescriptions.Middlewares
{
    public static class GlobalHandlerExtension
    {

        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }

    }
}
