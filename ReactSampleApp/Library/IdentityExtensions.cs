using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ReactSampleApp
{
    public static class IdentityExtensions
    {
        public static long GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier);
            return (claim != null) ? Convert.ToInt64(claim.Value) : 0;
        }
        public static string GetEmail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
