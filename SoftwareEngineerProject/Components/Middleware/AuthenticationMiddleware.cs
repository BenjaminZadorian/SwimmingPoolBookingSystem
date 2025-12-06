using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace SoftwareEngineerProject.Components.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        // Dictionary that represents the current user after the user is validated, stored as claim principle
        public static IDictionary<Guid, ClaimsPrincipal> Login { get; private set; } = new ConcurrentDictionary<Guid, ClaimsPrincipal>();

        public AuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check that if you are on a login page and you are accessing a users login page with a key
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
            {
                // Collect the key and the claim
                var key = Guid.Parse(context.Request.Query["key"]);
                var claim = Login[key];

                // Use claim to sign in with cookie authentication
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);
                // Redirect to home page
                context.Response.Redirect("/");
            } else 
            {
                await next(context);
            }


        }
    }
}
