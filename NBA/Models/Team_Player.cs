using Microsoft.EntityFrameworkCore;

namespace NBA.API.Models
{
    [Keyless]
    public class Team_Player
    {
        //public Team Team { get; set; }
        public int TeamID { get; set; }

        //public Player Player { get; set; }
        public int PlayerID { get; set; }
    }
}
