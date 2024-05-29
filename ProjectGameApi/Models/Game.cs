using GuessTheArtist;

namespace ProjectGameApi.Models
{
    public class Game
    {
        public List<Player> Player { get; set; }
        public Artist CurrentArtist { get; set; }
        public Dictionary<string, int> PlayerTrackIndices { get; set; } = new Dictionary<string, int>();
    }
}
