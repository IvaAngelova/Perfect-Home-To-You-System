using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PerfectHomeToYou.Data
{
    public class PerfectHomeToYouDbContext : IdentityDbContext
    {
        public PerfectHomeToYouDbContext(DbContextOptions<PerfectHomeToYouDbContext> options)
            : base(options)
        {
        }
    }
}
