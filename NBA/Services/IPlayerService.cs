using Microsoft.AspNetCore.Mvc;
using NBA.API.Models;

namespace NBA.API.Services
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllPlayers();
        Task<Player> GetPlayer(int id);
    }
}
