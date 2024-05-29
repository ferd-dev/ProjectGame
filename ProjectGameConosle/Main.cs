using GuessTheArtist;
using System.Collections.Generic;
using System.Globalization;
using ProjectGameConosle.DataAccess;


class MainProgrma
{
    static int Points = 0;
    static JsonDataAdapter DataAdapter = new JsonDataAdapter();

    static void Main(string[] args)
    {
        List<Genre> genres = DataAdapter.GetGenres();

        Console.WriteLine(genres);


        MainMenu menu = new MainMenu(genres);

        string value;
        do
        {
            PrintPoints();
            value = menu.print();

            if (value != null)
            {
                value = CapitalizeInput(value);

                if (value != "N")
                {
                    Console.Clear();
                    Console.WriteLine($"Seleccionaste {value}");

                    Artist artist = DataAdapter.GetRandomArtistsByGenre(value);
                    TrackManager trackManager = new TrackManager(artist);
                    bool verifyArtist = trackManager.VerifyArtist();
                    AddPoint(verifyArtist);

                    if (!KeepPlaying())
                    {
                        EndGaame();
                    }
                }
            }
        } while (value != "N");
    }

    static bool KeepPlaying()
    {
        string response;
        do
        {
            Console.WriteLine("Do you want to continue playing? [Y/N]");
            response = Console.ReadLine();
        } while (response != "Y" && response != "N");

        return response == "N" ? false : true;
    }

    static void EndGaame()
    {
        Console.WriteLine("The application is ending...");
        Environment.Exit(0);
    }

    static void PrintPoints()
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"|    Points: {Points}        |");
        Console.WriteLine("------------------------------");
    }

    static void AddPoint(bool state)
    {
        Points = state ? +1 : +0;
    }

    static string CapitalizeInput(string input)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        string capitalizedInput = textInfo.ToTitleCase(input);
        return capitalizedInput;
    }
}