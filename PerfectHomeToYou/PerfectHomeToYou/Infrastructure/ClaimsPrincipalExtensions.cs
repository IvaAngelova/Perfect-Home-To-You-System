﻿using System.Security.Claims;

using static PerfectHomeToYou.WebConstants;
using static PerfectHomeToYou.Areas.Admin.AdminConstants;

namespace PerfectHomeToYou.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
