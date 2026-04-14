using Microsoft.EntityFrameworkCore;
using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;

namespace Simple_Game_Store_WEB_API.Services.Genres
{
    public class GenreService : IGenreService
    {
        private readonly GameStoreContext dbContext;
        private readonly IGenreMapper genreMapper;

        public GenreService(GameStoreContext dbContext, IGenreMapper genreMapper)
        {
            this.dbContext = dbContext;
            this.genreMapper = genreMapper;
        }

        public async Task<Result<GenreDTO>> CreateGenreAsync(CreateGenreDTO createGenreDTO)
        {
            Genre newGenre = genreMapper.ToEntity(createGenreDTO);

            dbContext.Genres.Add(newGenre);

            await dbContext.SaveChangesAsync();

            return Result<GenreDTO>.Success(genreMapper.ToDTO(newGenre));
        }

        public async Task<Result> UpdateGenreAsync(int ID, UpdateGenreDTO updatedGenreDTO)
        {
            Genre? existingGenre = await dbContext.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.ID == ID);

            if (existingGenre is null)
                return Result.Failure(GenreErrors.GenreNotFound);

            existingGenre = genreMapper.ToEntity(updatedGenreDTO);
            existingGenre.ID = ID;

            dbContext.Genres.Update(existingGenre);

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public Task<Result> DeleteGenreAsync(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
