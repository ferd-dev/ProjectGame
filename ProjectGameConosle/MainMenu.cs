using GuessTheArtist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheArtist
{
    internal class MainMenu : IMenu
    {
        private List<Genre> _genres;

        public MainMenu(List<Genre> genres) {
            try
            {
                if (genres == null) {
                    throw new ArgumentNullException("There are no genres to show.");
                }
                _genres = genres;
            }
            catch (ArgumentNullException ex) 
            {
                Log.Error(ex.Message, "MainMenu.cs");
                Console.WriteLine(ex.Message);
            }
            
        }
        public string print()
        {
            PrintHead();
            PrintGenre();
            PrintFoot();

            return ReadEntry();
        }

        private void PrintHead()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("///////  WELCOME TO GUESS THE ARTIST  ///////");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("We'll see how well you know your favorite artists... :)");
            Console.WriteLine("---------------------------------------------");
        }

        private void PrintGenre()
        {
            Console.WriteLine("Enter the musical genre: ");

            foreach (Genre genre in _genres)
            {
                Console.WriteLine("- " + genre.Name);
            }
        }

        private void PrintFoot()
        {
            Console.WriteLine("- Press N to End Game");
        }

        private string ReadEntry()
        {
            string entry = Console.ReadLine();
            if (entry == "N") {
                return entry;
            }
            if (!GenreExists(entry))
            {
                return null;
            }
            return entry;
        }

        private bool GenreExists(string genreName)
        {
            return _genres.Exists(genre => genre.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
