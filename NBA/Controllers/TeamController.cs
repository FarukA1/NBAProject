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
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            var teams = await _teamService.GetAllTeams();

            if (teams == null)
            {
                return NotFound();
            }

            return Ok(teams);
        }

        [HttpGet("details")]
        public async Task<ActionResult<List<TeamDetail>>> GetTeamsDetail()
        {
            var teams = await _teamService.TeamsDetail();

            if (teams == null)
            {
                return NotFound();
            }

            return Ok(teams);
        }

        [HttpGet("{id}/mvp")]
        public async Task<ActionResult<Player>> GetTeamMVP(int id)
        {
            var player = await _teamService.GetTeamMVP(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _teamService.GetTeam(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        [HttpGet("{id}/games")]
        public async Task<ActionResult<List<Game>>> GetTeamGames(int id)
        {
            var games = await _teamService.GetTeamGames(id);

            if (games == null)
            {
                return NotFound();
            }

            return games;
        }

        [HttpGet("{id}/biggesthomewin")]
        public async Task<ActionResult<Game>> BiggestHomeWin(int id)
        {
            var game = await _teamService.BiggestHomeWin(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}/biggesthomeloss")]
        public async Task<ActionResult<Game>> BiggestHomeLoss(int id)
        {
            var game = await _teamService.BiggestHomeLoss(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}/biggestawaywin")]
        public async Task<ActionResult<Game>> BiggestAwayWin(int id)
        {
            var game = await _teamService.BiggestAwayWin(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}/biggestawayloss")]
        public async Task<ActionResult<Game>> BiggestAwayLoss(int id)
        {
            var game = await _teamService.BiggestAwayLoss(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}/biggestwon")]
        public async Task<ActionResult<Game>> BiggestWin(int id)
        {
            var biggestHomeWin = await _teamService.BiggestHomeWin(id);
            var bigestAwayWin = await _teamService.BiggestAwayWin(id);

            var game = await _teamService.BiggestWin(biggestHomeWin, bigestAwayWin);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}/biggestloss")]
        public async Task<ActionResult<Game>> BiggestLoss(int id)
        {
            var biggestHomeLoss = await _teamService.BiggestHomeLoss(id);
            var bigestAwayLoss = await _teamService.BiggestAwayLoss(id);

            var game = await _teamService.BiggestLoss(biggestHomeLoss, bigestAwayLoss);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("{id}/latestgame")]
        public async Task<ActionResult<Game>> LatestGame(int id)
        {
            var game = await _teamService.LatestGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        //// PUT: api/Team/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTeam(int id, Team team)
        //{
        //    if (id != team.TeamID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(team).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TeamExists(id))
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

        //// POST: api/Team
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Team>> PostTeam(Team team)
        //{
        //  if (_context.Teams == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
        //  }
        //    _context.Teams.Add(team);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTeam", new { id = team.TeamID }, team);
        //}

        //// DELETE: api/Team/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTeam(int id)
        //{
        //    if (_context.Teams == null)
        //    {
        //        return NotFound();
        //    }
        //    var team = await _context.Teams.FindAsync(id);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Teams.Remove(team);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TeamExists(int id)
        //{
        //    return (_context.Teams?.Any(e => e.TeamID == id)).GetValueOrDefault();
        //}
    }
}
