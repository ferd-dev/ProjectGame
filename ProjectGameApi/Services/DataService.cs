using GuessTheArtist.DataAccess;
using GuessTheArtist;

namespace ProjectGameApi.Services
{
    public class DataService
    {
        private Database _database;

        public DataService()
        {
            JsonDataAdapter jsonDataAdapter = new JsonDataAdapter();
            _database = jsonDataAdapter.GetDatabase();
        }

        public Database GetData()
        {
            return _database;
        }

        public List<String> GetGenres()
        {
            List<string> genres = new List<string>();
            try
            {
                foreach (var genre in _database.Genres)
                {
                    genres.Add(genre.Name);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "DataManager.cs");
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            return genres;
        }

        public Artist GetRandomArtistsByGenre(string genreName)
        {
            try
            {
                var genre = _database.Genres.FirstOrDefault(
                    g => g.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase)
                );

                Random random = new Random();
                int randomIndex = random.Next(0, genre.Artists.Length);
                Artist randomArtist = genre.Artists[randomIndex];

                return randomArtist;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "DataManager.cs");
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public List<Artist> GetArtistsByGenre(string genreName)
        {
            try
            {
                var genre = _database.Genres.FirstOrDefault(
                    g => g.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase)
                );

                if (genre == null || genre.Artists == null || genre.Artists.Length == 0)
                {
                    return new List<Artist>();
                }

                return genre.Artists.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "DataManager.cs");
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Artist>();
            }
        }

        public Artist GetArtistByName(string artistName)
        {
            try
            {
                foreach (var genre in _database.Genres)
                {
                    var artist = genre.Artists.FirstOrDefault(
                        a => a.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase)
                    );

                    if (artist != null)
                    {
                        return artist;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "DataManager.cs");
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
