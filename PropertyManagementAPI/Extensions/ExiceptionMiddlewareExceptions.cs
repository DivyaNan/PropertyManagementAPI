using Microsoft.AspNetCore.Diagnostics;
using PropertyManagementAPI.Middleware;

namespace PropertyManagementAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void CustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

        }
        public static void CustomBuitInExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler(

                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = 500;
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    await context.Response.WriteAsync(ex.Error.Message);
                                }
                            }
                            );
                    }
                );
            }

        }
    }
}
