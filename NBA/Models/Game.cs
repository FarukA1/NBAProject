namespace NBA.API.Models
{
    public class Game
    {
        public int GameID { get; set; }
        //public Team Team { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public DateTime GameDateTime { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set;}
        //public Player Player { get; set; }
        public int MVPPLayerID { get; set; }

    }
}
