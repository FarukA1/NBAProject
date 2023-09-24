namespace NBA.API.Models
{
    public class TeamDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Stadium { get; set; }
        public string Logo { get; set; }
        public string URL { get; set; }
        public string MVP { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int PlayedHome { get; set; }
        public int PlayedAway { get; set; }
        public string BiggestWin { get; set; }
        public string BiggestLoss { get; set; }
        public string LastGameStadium { get; set; }
        public DateTime LastGameDate { get; set; }
    }
}
