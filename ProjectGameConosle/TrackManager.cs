using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GuessTheArtist
{
    internal class TrackManager
    {
        private Artist _artist;
        private int _trackNumber;
        private List<int> _shownTracksIndexes;
        private Random _random;

        public TrackManager(Artist artist) { 
            _artist = artist;
            _trackNumber = 0;
            _shownTracksIndexes = new List<int>();
            _random = new Random();
        }

        public bool VerifyArtist()
        {
            while (_trackNumber < _artist.Tracks.Length)
            {
                _trackNumber++;
                Console.Clear();
                PrintTrackNumber();
                PrintTrack(_artist.Tracks[_trackNumber - 1]);

                Console.Write("Your answer: ");
                string answer = Console.ReadLine();

                if (VerifyAnswer(answer))
                {
                    PrintSuccessMessage();
                    return true;
                }
            }

            PrintErrorMessage();

            return false;
        }

        private void PrintSuccessMessage()
        {
            Console.WriteLine("Congratulations :) the artist managed to guess");
        }

        private void PrintErrorMessage()
        {
            Console.WriteLine("Lost :(, better luck next time");
        }

        private bool VerifyAnswer(string answer)
        {
            string artistName = _artist.Name.ToLower();
            answer = answer.ToLower();

            return (answer == artistName) ? true : false;
        }

        private void PrintTrack(string track)
        {
            Console.WriteLine(track);
        }

        private void PrintTrackNumber()
        {
            Console.WriteLine($"Track #: {_trackNumber}  de {_artist.Tracks.Length}");
            Console.WriteLine("------------------------------------------");
        }
    }
}
