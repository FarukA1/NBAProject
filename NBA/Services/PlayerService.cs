using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.API.Data;
using NBA.API.Models;

namespace NBA.API.Services
{
    public class PlayerService : IPlayerService
    {
        public readonly ApplicationDbContext _context;
        public readonly IGameService _gameService;

        public PlayerService(ApplicationDbContext context, IGameService gameService)
        {
            _context = context;
            _gameService = gameService;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            var players = await _context.Players.ToListAsync();
            return players;
        }

        public async Task<Player> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            return player;
        }
    }
}
