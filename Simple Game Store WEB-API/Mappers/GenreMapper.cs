using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public class GenreMapper : IGenreMapper
    {
        public Genre ToEntity(CreateGenreDTO createGenreDTO) // Converts CreateGenreDTO To Genre Entity
        {
            return new Genre
            {
                Name = createGenreDTO.Name
            };
        }

        public Genre ToEntity(UpdateGenreDTO updateGenreDTO) // Converts UpdateGenreDTO To Genre Entity
        {
            return new Genre
            {
                Name = updateGenreDTO.Name
            };
        }



        public GenreDTO ToDTO(Genre genre) // Converts Genre Entity To GenreDTO
        {
            return new GenreDTO(
                ID: genre.ID,
                Name: genre.Name
            );
        }
    }
}
