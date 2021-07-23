namespace PerfectHomeToYou.Services.Cities
{
    public interface ICityServices
    {
        CityQueryServiceModel All(string searchTerm,
           int currentPage,
           int citiesPerPage);
    }
}
