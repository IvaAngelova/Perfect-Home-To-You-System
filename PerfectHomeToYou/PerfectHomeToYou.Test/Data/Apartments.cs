using System.Linq;
using System.Collections.Generic;

using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Test.Data
{
    public static class Apartments
    {
        public static IEnumerable<Apartment> TenPublicApartments()
            => Enumerable.Range(0, 10).Select(i => new Apartment
            {
                IsPublic = true
            });
    }
}
