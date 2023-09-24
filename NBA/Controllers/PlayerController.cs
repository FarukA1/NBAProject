using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.API.Data;
using NBA.API.Models;
using NBA.API.Services;

namespace NBA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetPlayers()
        {
            var players = await _playerService.GetAllPlayers();

            if (players == null)
            {
                return NotFound();
            }

            return Ok(players);
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayer(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        //// PUT: api/Player/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPlayer(int id, Player player)
        //{
        //    if (id != player.PlayerID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(player).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PlayerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Player
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Player>> PostPlayer(Player player)
        //{
        //  if (_context.Players == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Players'  is null.");
        //  }
        //    _context.Players.Add(player);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPlayer", new { id = player.PlayerID }, player);
        //}

        //// DELETE: api/Player/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePlayer(int id)
        //{
        //    if (_context.Players == null)
        //    {
        //        return NotFound();
        //    }
        //    var player = await _context.Players.FindAsync(id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Players.Remove(player);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PlayerExists(int id)
        //{
        //    return (_context.Players?.Any(e => e.PlayerID == id)).GetValueOrDefault();
        //}
    }
}
