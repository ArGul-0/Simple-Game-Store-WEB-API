using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Services.Genres
{
    public interface IGenreService
    {
        public Task<Result<GenreDTO>> CreateGenreAsync(CreateGenreDTO createGenreDTO); // Method For Creating A New Genre
        public Task<Result> UpdateGenreAsync(int ID, UpdateGenreDTO updatedGenreDTO); // Method For Updating An Existing Genre By ID
        public Task<Result> DeleteGenreAsync(int ID); // Method For Deleting An Existing Genre By ID
    }
}
