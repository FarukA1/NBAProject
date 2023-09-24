namespace NBA.UI.Model
{
    public class Team
    {
        public int id { get; set; }
        public string name { get; set; }
        public string stadium { get; set; }
        public string logo { get; set; }
        public string url { get; set; }
        public string mvp { get; set; }
        public int played { get; set; }
        public int won { get; set; }
        public int lost { get; set; }
        public int playedHome { get; set; }
        public int playedAway { get; set; }
        public string biggestWin { get; set; }
        public string biggestLoss { get; set; }
        public string lastGameStadium { get; set; }
        public DateTime lastGameDate { get; set; }
    }
}
