namespace PerfectHomeToYou.Services.Neighborhoods
{
    public interface INeighborhoodServices
    {
        NeighborhoodQueryServiceModel All(string searchTerm,
           int currentPage,
           int neighborhoodsPerPage);
    }
}
