using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public class GenreMapper : IGenreMapper
    {
        public GenreDTO ToDTO(Genre genre)
        {
            return new GenreDTO(
                ID: genre.ID,
                Name: genre.Name
            );
        }
    }
}
