using System.Security.Claims;

namespace EndPoint.MVC.WebFramework
{
    public static class GetUserID
    {
        public static int GetUserId(this ClaimsPrincipal principal) =>
           int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
