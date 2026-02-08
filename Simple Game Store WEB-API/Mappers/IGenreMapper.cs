using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public interface IGenreMapper
    {
        /// <summary>
        /// Converts CreateGenreDTO To Genre Entity
        /// </summary>
        /// <returns>Genre</returns>
        public Genre ToEntity(CreateGenreDTO createGenreDTO);
        /// <summary>
        /// Converts UpdateGenreDTO To Genre Entity
        /// </summary>
        /// <returns>Genre</returns>
        public Genre ToEntity(UpdateGenreDTO updateGenreDTO);



        /// <summary>
        /// Converts Genre Entity to GenreDTO
        /// </summary>
        /// <returns>GenreDTO</returns>
        public GenreDTO ToDTO(Genre genre);
    }
}
