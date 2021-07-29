using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Services.Neighborhoods;

namespace PerfectHomeToYou.Models.Neighborhoods
{
    public class AllNeighborhoodsQueryModel
    {
        public const int NeighborhoodsPerPage = 3;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalNeighborhoods { get; set; }

        public IEnumerable<NeighborhoodServiceModel> Neighborhoods { get; set; }
    }
}
