using Microsoft.AspNetCore.Mvc;
using NBA.API.Models;

namespace NBA.API.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGames();
        Task<Game> GetGame(int id);
        Task<List<Game>> GetTeamHomeGame(int homeTeamID);
        Task<List<Game>> GetTeamAwayGame(int awayTeamID);
        //Task<List<Game>> GetTeamGames(int teamID);
    }
}
