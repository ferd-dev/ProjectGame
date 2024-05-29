using GuessTheArtist;
using GuessTheArtistApi.Managers;
using Microsoft.AspNetCore.Mvc;
using ProjectGameApi.Models;
using ProjectGameApi.Requests;
using ProjectGameApi.Services;


namespace ProjectGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private DataService _dataService = new DataService();
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start")]
        public ActionResult<Player> StartGame([FromBody] StartGameRequest request)
        {
            var newPlayer = _gameService.StartGame(request.Name);
            return Ok(newPlayer);
        }

        [HttpGet("players")]
        public ActionResult<IEnumerable<string>> GetPlayers()
        {
            var data = _gameService.GetPlayers();
            return Ok(data);
        }

        [HttpGet("Genres")]
        public ActionResult<IEnumerable<string>> Genres()
        {
            var data = _dataService.GetGenres();
            return Ok(data);
        }

        [HttpGet("RandomArtistsByGenre/{Genre}")]
        public ActionResult<IEnumerable<string>> RandomArtistsByGenre(
            string Genre, 
            [FromHeader(Name = "X-Token")] string Token
        )
        {
            var artists = _dataService.GetRandomArtistsByGenre(Genre);
            _gameService.SetCurrentArtist(Token, artists);
            return Ok(artists);
        }

        [HttpGet("nextTrack/")]
        public ActionResult<IEnumerable<string>> GetTrack([FromHeader(Name = "X-Token")] string Token)
        {
            
            var nextTrack = _gameService.GetTrack(Token);

            return Ok(nextTrack);
        }

        [HttpPost("verifyArtist")]
        public ActionResult<string> VerifyArtist(
            [FromBody] VerifyArtistRequest request,
            [FromHeader(Name = "X-Token")] string Token
        )
        {
            var message = _gameService.VerifyArtist(Token, request.ArtistName);
            return Ok(message);
        }
    }
}
