﻿using System.Collections.Generic;

namespace PerfectHomeToYou.Models.Home
{
    public class IndexViewModel
    {
        public int TotalApartments { get; init; }

        public int TotalUsers { get; init; }

        public List<ApartmentIndexViewModel> Apartments { get; set; }
    }
}