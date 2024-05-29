using GuessTheArtist;
using ProjectGameApi.Models;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectGameApi.Services
{
    public class GameService
    {
        private readonly Dictionary<string, Game> _games;

        public GameService()
        {
            _games = new Dictionary<string, Game>();
        }

        public Player StartGame(string namePlayer)
        {
            var playerId = Guid.NewGuid().ToString();
            var player = new Player
            {
                Id = playerId,
                Name = namePlayer,
                Punctuation = 0
            };

            var game = new Game
            {
                Player = new List<Player> { player },
                CurrentArtist = null
            };

            _games[playerId] = game;
            return player;
        }

        public Dictionary<string, Game> GetPlayers()
        {
            return _games;
        }

        public void SetCurrentArtist(string playerId, Artist artist)
        {
            if (_games.TryGetValue(playerId, out var game))
            {
                game.CurrentArtist = artist;
                game.PlayerTrackIndices.Remove(playerId);
            }
        }


        public string GetTrack(string playerId)
        {
            if (_games.TryGetValue(playerId, out var game) && game.CurrentArtist != null)
            {
                if (!game.PlayerTrackIndices.ContainsKey(playerId))
                {
                    game.PlayerTrackIndices[playerId] = 0;
                }

                var trackIndex = game.PlayerTrackIndices[playerId];

                var artist = game.CurrentArtist;
                var totalTracks = artist.Tracks.Length;

                if (trackIndex < totalTracks)
                {
                    var currentTrack = artist.Tracks[trackIndex];

                    game.PlayerTrackIndices[playerId] = trackIndex + 1;
                    

                    return currentTrack;
                }
            }
            return "Lost :( the clues are finished";
        }

        public string VerifyArtist(string playerId, string artistName)
        {
            if (_games.TryGetValue(playerId, out var game) && game.CurrentArtist != null && game.Player != null)
            {
                var currentArtistName = game.CurrentArtist.Name;

                if (string.Equals(currentArtistName, artistName, StringComparison.OrdinalIgnoreCase))
                {
                    AddPoint(playerId);
                    return "Correct! :) You guessed the artist correctly.";
                }
            }

            return "Keep trying! The artist's name is not correct. :(";
        }

        public void AddPoint(string playerId)
        {
            if (_games.TryGetValue(playerId, out var game))
            {
                game.Player[0].Punctuation++;
            }
        }
    }
}
