using Drug_Procurement.Middlewares;

namespace Drug_Procurement.Extension;

public static class BuildExtension
{
    public static void ExtendBuilder(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleWare>();
    }
}
