using Enumbers_server.Api.Dtos;

namespace Enumbers_server.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPoint = "GetGameById";

    private static readonly List<GameDto> games = [
        new GameDto(1, "Assassin's Creed Valhalla", "Action-Adventure", 59.99m, new DateOnly(2020, 11, 10)),
        new GameDto(2, "The Witcher 3: Wild Hunt", "Role-Playing", 39.99m, new DateOnly(2015, 5, 19)),
        new GameDto(3, "Call of Duty: Warzone", "Battle Royale", 0m, new DateOnly(2020, 3, 10)),
        new GameDto(4, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
        new GameDto(5, "FIFA 22", "Sports", 59.99m, new DateOnly(2021, 10, 1)),
        new GameDto(6, "Among Us", "Social Deduction", 4.99m, new DateOnly(2018, 6, 15))
    ];

    // this method is used to map the endpoints for the games resource
    // this keyword is used to define an extension method for the WebApplication class
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () => games);


        //GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            return games.Find(game => game.Id == id);
        }).WithName(GetGameEndPoint);
        // WithName method is used to give a name to the route so that it can be referred to in other parts of the application.

        //POST /games
        group.MapPost("/", (CreateGameDto GameDto) =>
        {
            var newGame = new GameDto(games.Count + 1, GameDto.Name, GameDto.Genre, GameDto.Price, GameDto.ReleaseDate);
            games.Add(newGame);
            return Results.CreatedAtRoute(GetGameEndPoint, new { id = newGame.Id }, newGame);
            // CreatedAtRoute method is used to return a 201 Created status code along with the newly created resource.
        });


        group.MapPut("/{id}", (int id, UpdateGameDto GameDto) =>
        {
            var existingGameIndex = games.FindIndex(game => game.Id == id);
            if (existingGameIndex == -1)
            {
                return Results.NotFound();
            }
            var updatedGame = new GameDto(id, GameDto.Name, GameDto.Genre, GameDto.Price, GameDto.ReleaseDate);
            games[existingGameIndex] = updatedGame;

            return Results.NoContent();
            // NoContent method is used to return a 204 No Content status code.
        });

        return group;
    }
}
