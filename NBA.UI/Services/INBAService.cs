using NBA.UI.Model;

namespace NBA.UI.Services
{
    public interface INBAService
    {
        Task<List<Team>> GetTeamsDetail();
    }
}
