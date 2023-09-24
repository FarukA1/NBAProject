using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA.API.Data;
using NBA.API.Models;

namespace NBA.API.Services
{
    public class TeamService : ITeamService
    {
        public readonly ApplicationDbContext _context;
        public readonly IGameService _gameService;
        public readonly IPlayerService _playerService;

        public TeamService(ApplicationDbContext context, IGameService gameService, IPlayerService playerService)
        {
            _context = context;
            _gameService = gameService;
            _playerService = playerService;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            return teams;
        }

        public async Task<Team> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            return team;
        }

        public async Task<List<Player>> GetTeamPlayers(int teamID)
        {
            List<Player> playersDetail = new List<Player>();   
            var players = await _context.Team_Player.Where(x => x.TeamID == teamID).ToListAsync();

            foreach (var player in players)
            {
                var playerDetail = await _playerService.GetPlayer(player.PlayerID);
                playersDetail.Add(playerDetail);
            }
            return playersDetail;
        }

        public async Task<Player> GetTeamMVP(int teamID)
        {
            Player mvpPlayer = new Player();

            var allgames = await GetTeamGames(teamID);

            List<Game> teamGamesMvp = new List<Game>();

            List<int> playerIds = new List<int>();

            if(allgames != null)
            {
                //Only use game where team our play was the MVP

                var allTeamPlayers = await GetTeamPlayers(teamID);

                foreach (var game in allgames)
                {
                    foreach(var player  in allTeamPlayers)
                    {
                        if(game.MVPPLayerID == player.PlayerID)
                        {
                            teamGamesMvp.Add(game);
                        }
                    }
                }

            }

            playerIds = teamGamesMvp.ToList().Select(t => (int)t.MVPPLayerID).ToList();

            var mvpId = playerIds.GroupBy(n => n)
            .OrderByDescending(group => group.Count())
            .First()
            .Key;

            mvpPlayer = await _playerService.GetPlayer(mvpId);

            return mvpPlayer;
        }

        public async Task<List<Game>> GetTeamGames(int teamID)
        {
            List<Game> allGames = new List<Game>();

            var homeGames = await _gameService.GetTeamHomeGame(teamID);
            var awayGames = await _gameService.GetTeamAwayGame(teamID);

            allGames.AddRange(homeGames);
            allGames.AddRange(awayGames);

            return allGames;
        }

        public async Task<Game> BiggestHomeWin(int teamID)
        {
            var allGames = await _gameService.GetTeamHomeGame(teamID);

            Game biggestGame = new Game();

            int largestWinDifference = 0;

            foreach(var game in allGames)
            {
                int difference = game.HomeScore - game.AwayScore;

                if(difference > largestWinDifference)
                {
                    largestWinDifference = difference;
                    biggestGame = game;
                }
            }

            return biggestGame;
        }

        public async Task<Game> BiggestHomeLoss(int teamID)
        {
            var allGames = await _gameService.GetTeamHomeGame(teamID);

            Game biggestGame = new Game();

            int largestWinDifference = 0;

            foreach (var game in allGames)
            {
                int difference = game.HomeScore - game.AwayScore;

                if (difference < largestWinDifference)
                {
                    largestWinDifference = difference;
                    biggestGame = game;
                }
            }

            return biggestGame;
        }

        public async Task<Game> BiggestAwayWin(int teamID)
        {
            var allGames = await _gameService.GetTeamAwayGame(teamID);

            Game biggestGame = new Game();

            int largestWinDifference = 0;

            foreach (var game in allGames)
            {
                int difference = game.AwayScore - game.HomeScore;

                if (difference > largestWinDifference)
                {
                    largestWinDifference = difference;
                    biggestGame = game;
                }
            }

            return biggestGame;
        }

        public async Task<Game> BiggestAwayLoss(int teamID)
        {
            var allGames = await _gameService.GetTeamAwayGame(teamID);

            Game biggestGame = new Game();

            int largestWinDifference = 0;

            foreach (var game in allGames)
            {
                int difference = game.AwayScore - game.HomeScore;

                if (difference < largestWinDifference)
                {
                    largestWinDifference = difference;
                    biggestGame = game;
                }
            }

            return biggestGame;
        }

        public async Task<Game> BiggestWin(Game biggestHomeWin, Game bigestAwayWin)
        {

            int homeWinDiff = biggestHomeWin.HomeScore - biggestHomeWin.AwayScore;
            int awayWinDiff = bigestAwayWin.AwayScore - bigestAwayWin.HomeScore;

            if(homeWinDiff > awayWinDiff)
            {
                return biggestHomeWin;
            }
            else
            {
                return bigestAwayWin;
            }

        }

        public async Task<Game> BiggestLoss(Game biggestHomeLoss, Game bigestAwayLoss)
        {
            int homeLossDiff = biggestHomeLoss.AwayScore - biggestHomeLoss.HomeScore;
            int awayLossDiff = bigestAwayLoss.AwayScore - bigestAwayLoss.HomeScore;

            if (homeLossDiff > awayLossDiff)
            {
                return bigestAwayLoss;
            }
            else
            {
                return biggestHomeLoss;
            }

        }

        public async Task<Game> LatestGame(int id)
        {
            var allGames = await GetTeamGames(id);

            Game latestGame = new Game();
            DateTime greatestDate = DateTime.MinValue;

            foreach (var game in allGames)
            {
                if(game.GameDateTime > greatestDate)
                {
                    greatestDate = game.GameDateTime;
                    latestGame = game;
                }
            }

            return latestGame;
        }

        public async Task<string> LastGameStadium(Game game)
        {
            var team = await GetTeam(game.HomeTeamID);

            return team.Stadium;
        }

        public async Task<List<TeamDetail>> TeamsDetail()
        {
            List<TeamDetail> allTeamsDetail = new List<TeamDetail>();

            var teams = await GetAllTeams();

            if(teams != null)
            {
                foreach (var team in teams)
                {
                    var teamDetail = await TeamDetail(team);
                    allTeamsDetail.Add(teamDetail);
                }
            }

            return allTeamsDetail;
        }
        public async Task<TeamDetail> TeamDetail(Team team)
        {

            TeamDetail detail = new TeamDetail();

            var t = await GetTeam(team.TeamID);

            detail.Id = t.TeamID;
            detail.Name = t.Name;
            detail.Stadium = t.Stadium;
            detail.Logo = t.Logo;
            detail.URL = t.URL;

            var mvp = await GetTeamMVP(team.TeamID);
                    
            if(mvp != null)
            {
                detail.MVP = mvp.Name;
            }

            var allGames = await GetTeamGames(t.TeamID);

            detail.Played = allGames.Count();
            int won = 0;
            int lost = 0;

            foreach (var game in allGames)
            {
                if (game.HomeScore > game.AwayScore)
                {
                    won++;
                }
                else
                {
                    lost++;
                }
            }

            detail.Won = won;
            detail.Lost = lost;

            var homeGames = await _gameService.GetTeamHomeGame(t.TeamID);
            var awayGames = await _gameService.GetTeamAwayGame(t.TeamID);

            detail.PlayedHome = homeGames.Count();
            detail.PlayedAway = awayGames.Count();

            var biggestHomeWin = await BiggestHomeWin(t.TeamID);
            var biggestAwayWin = await BiggestAwayWin(t.TeamID);

            var biggestWon = await BiggestWin(biggestHomeWin, biggestAwayWin);

            if (biggestWon.HomeScore > biggestWon.AwayScore)
            {
                //If bigger won was on home ground
                detail.BiggestWin = $"{biggestWon.HomeScore}-{biggestWon.AwayScore}";
            }
            else
            {
                //If bigger won was on away ground
                detail.BiggestWin = $"{biggestWon.AwayScore}-{biggestWon.HomeScore}";
            }

            var biggestHomeLoss = await BiggestHomeLoss(t.TeamID);
            var biggestAwayLoss = await BiggestAwayLoss(t.TeamID);

            var biggestLoss = await BiggestLoss(biggestHomeLoss, biggestAwayLoss);

            if (biggestLoss.HomeScore < biggestWon.AwayScore)
            {
                //This is an home loss
                detail.BiggestLoss = $"{biggestLoss.HomeScore}-{biggestLoss.AwayScore}";
            }
            else
            {
                //This is an away loss
                detail.BiggestLoss = $"{biggestLoss.AwayScore}-{biggestLoss.HomeScore}";
            }

            var latestGame = await LatestGame(t.TeamID);

            detail.LastGameDate = latestGame.GameDateTime;

            var stadium = await LastGameStadium(latestGame);

            if(stadium != null)
            {
                detail.LastGameStadium = stadium;
            }

        return detail;

        }
    }
}
