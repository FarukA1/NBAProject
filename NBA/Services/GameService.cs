using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.API.Data;
using NBA.API.Models;

namespace NBA.API.Services
{
    public class GameService : IGameService
    {
        public readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllGames()
        {
            var games = await _context.Games.ToListAsync();
            return games;
        }

        public async Task<Game> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            return game;
        }

        public async Task<List<Game>> GetTeamHomeGame(int homeTeamID)
        {
            var teamHomeGames = await _context.Games.Where(x => x.HomeTeamID == homeTeamID).ToListAsync();
            return teamHomeGames;
        }

        public async Task<List<Game>> GetTeamAwayGame(int awayTeamID)
        {
            var teamAwayGames = await _context.Games.Where(x => x.AwayTeamID == awayTeamID).ToListAsync();
            return teamAwayGames;
        }
    }
}
