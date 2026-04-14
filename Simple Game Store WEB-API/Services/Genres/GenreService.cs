using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Services.Genres
{
    public class GenreService : IGenreService
    {
        public Task<Result<GenreDTO>> CreateGenreAsync(CreateGenreDTO createGenreDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteGenreAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateGenreAsync(int ID, UpdateGenreDTO updateGenreDTO)
        {
            throw new NotImplementedException();
        }
    }
}
