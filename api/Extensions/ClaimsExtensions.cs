using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsExtensions //static to make dat shit extend
    {
        public static string GetUserNAme(this ClaimsPrincipal user)
        { // this is how we reach into the claim, which we get from JWT token
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value; //???
        }
    }
}