using Microsoft.AspNetCore.Mvc;
using NBA.API.Models;

namespace NBA.API.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeams();
        Task<Team> GetTeam(int id);
        Task<List<Player>> GetTeamPlayers(int teamID);
        Task<Player> GetTeamMVP(int teamID);
        Task<List<Game>> GetTeamGames(int teamID);
        Task<Game> BiggestHomeWin(int teamID);
        Task<Game> BiggestHomeLoss(int teamID);
        Task<Game> BiggestAwayWin(int teamID);
        Task<Game> BiggestAwayLoss(int teamID);
        Task<Game> BiggestWin(Game biggestHomeWin, Game bigestAwayWin);
        Task<Game> BiggestLoss(Game biggestHomeLoss, Game bigestAwayLoss);
        Task<Game> LatestGame(int id);
        Task<string> LastGameStadium(Game game);

        Task<List<TeamDetail>> TeamsDetail();
        Task<TeamDetail> TeamDetail(Team team);
    }
}
