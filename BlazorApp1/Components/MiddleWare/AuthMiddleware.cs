using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace BlazorApp1.Components.MiddleWare
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _siguiente;

        public static IDictionary<Guid, ClaimsPrincipal> Logins { get; private set;  } = new ConcurrentDictionary<Guid, ClaimsPrincipal>();


        public AuthMiddleware(RequestDelegate siguiente)
        {
            _siguiente = siguiente ?? throw new ArgumentNullException(nameof(siguiente));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/" && context.Request.Query.ContainsKey("key")) {
                var key = Guid.Parse(context.Request.Query["key"]);
                var claim = Logins[key];
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);
                if (claim.IsInRole("Admin"))
                {
                    context.Response.Redirect("/Admin");
                }
                else if (claim.IsInRole("Maestro"))
                {
                    context.Response.Redirect("/Maestro");
                }
                else if (claim.IsInRole("Estudiante"))
                {
                    context.Response.Redirect("/Estudiante");
                }
                else
                {
                    context.Response.Redirect("/Home");
                }
            }
            else
            {
                await _siguiente(context);
            }
        }
            
        
    }
}
