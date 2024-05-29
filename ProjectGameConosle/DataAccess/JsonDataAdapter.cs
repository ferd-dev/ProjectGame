using GuessTheArtist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectGameConosle.DataAccess
{
    internal class JsonDataAdapter
    {
        private string _filePath = "../../../DataAccess/db.json";
        private Database _database = new Database();

        public JsonDataAdapter()
        {
            ReadDatabase();
        }

        private void ReadDatabase()
        {
            try
            {
                StreamReader reader = new StreamReader(_filePath);
                string json = reader.ReadToEnd();
                _database = JsonSerializer.Deserialize<Database>(json);
                VerifyThatTheDatabaseIsNotNull();
            }
            catch (FileNotFoundException)
            {
                Log.Error("Database file not found.", "JsonDataAdapter.cs");
                Console.WriteLine("Database file not found.");
            }
            catch (JsonException)
            {
                Log.Error("The database file is not valid JSON.", "JsonDataAdapter.cs");
                Console.WriteLine("The database file is not valid JSON.");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "JsonDataAdapter.cs");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                VerifyThatTheDatabaseIsNotNull();
                VerifyGenresInDatabase();

                foreach (var genre in _database.Genres)
                {
                    Genre newGenre = new Genre
                    {
                        Name = genre.Name,
                        Artists = genre.Artists.ToArray()
                    };

                    genres.Add(newGenre);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "JsonDataAdapter.cs");
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

                VerifyThatTheGenreExists(genre);
                VerifyThatThereAreArtistsInTheGenre(genre);

                Random random = new Random();
                int randomIndex = random.Next(0, genre.Artists.Length);
                Artist randomArtist = genre.Artists[randomIndex];

                return randomArtist;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "JsonDataAdapter.cs");
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private void VerifyGenresInDatabase()
        {
            if (_database.Genres == null)
            {
                Log.Error("No genres were found in the database.", "JsonDataAdapter.cs");
                throw new Exception("No genres were found in the database.");
            }
        }

        private void VerifyThatTheDatabaseIsNotNull()
        {
            if (_database == null)
            {
                Log.Error("The database is empty.", "JsonDataAdapter.cs");
                throw new Exception("The database is empty.");
            }
        }

        private void VerifyThatTheGenreExists(Genre genre)
        {
            if (genre == null)
            {
                Log.Error("The genus was not found in the database.", "JsonDataAdapter.cs");
                throw new Exception($"The genus was not found in the database.");
            }
        }

        private void VerifyThatThereAreArtistsInTheGenre(Genre genre)
        {
            if (genre.Artists == null || genre.Artists.Length == 0)
            {
                Log.Error("No artists found for the genre", "JsonDataAdapter.cs");
                throw new Exception("No artists found for the genre.");
            }
        }
    }
}
