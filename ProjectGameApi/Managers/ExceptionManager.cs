using GuessTheArtist;

namespace GuessTheArtistApi.Managers
{
    static class ExceptionManager
    {
        static public void VerifyGenresInDatabase(Genre[] genres)
        {
            if (genres == null)
            {
                Log.Error("No genres were found in the database.", "JsonDataAdapter.cs");
                throw new Exception("No genres were found in the database.");
            }
        }
    }
}
