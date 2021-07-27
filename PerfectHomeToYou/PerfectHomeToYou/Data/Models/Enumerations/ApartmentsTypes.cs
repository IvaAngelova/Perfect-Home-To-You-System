using System.ComponentModel.DataAnnotations;

namespace PerfectHomeToYou.Data.Models.Enumerations
{
    public enum ApartmentsTypes
    {
        Studio = 1,
        [Display(Name = "One Bedroom")]
        OneBedroom = 2,
        [Display(Name = "Two Bedroom")]
        TwoBedroom = 3,
        [Display(Name = "Three Bedroom")]
        ThreeBedroom = 4,
        Loft = 5,
        Duplex = 6,
        [Display(Name = "Garden Apartment")]
        GardenApartment = 7,
        Penthouse = 8
    }
}
