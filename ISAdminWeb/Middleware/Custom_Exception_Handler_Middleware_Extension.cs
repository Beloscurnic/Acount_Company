namespace ISAdminWeb.Middleware
{
    public static class Custom_Exception_Handler_Middleware_Extension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this
            IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Custom_Exception_Handler_Middleware>();
        }
    }
}
