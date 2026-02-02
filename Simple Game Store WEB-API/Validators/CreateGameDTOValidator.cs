using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Simple_Game_Store_WEB_API.Validators
{
    /// <summary>
    /// Validator For CreateGameDTO To Ensure Data Integrity When Creating A New Game.
    /// </summary>
    /// <remarks>
    /// This Validator Checks That The GenreID Exists In The Database. Depends On NuGet FluentValidation Package.
    /// </remarks>
    public class CreateGameDTOValidator : AbstractValidator<CreateGameDTO>
    {
        public CreateGameDTOValidator(GameStoreContext dbContext)
        {
            RuleFor(x => x.GenreID) // Validate GenreID
                .MustAsync(async (genreID, cancellation) => await dbContext.Genres.AnyAsync(g => g.ID == genreID, cancellation))
                .WithMessage((dto, id) => $"Genre with ID {id} does not exist.");
        }
    }
}
