using GuessTheArtistApi.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuessTheArtist.DataAccess
{
    internal class JsonDataAdapter
    {
        private string _filePath = "./DataAccess/db.json";
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
                _database = JsonSerializer.Deserialize<Database>(json) ?? new Database();
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

        public Database GetDatabase()
        {
            return _database;
        }
    }
}
