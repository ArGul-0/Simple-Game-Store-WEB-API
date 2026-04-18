using Simple_Game_Store_WEB_API.DTOs.Users;
using Simple_Game_Store_WEB_API.Entities;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public interface IUsersMapper
    {
        /// <summary>
        /// Maps A UserGame Entity To A UserGameDTO
        /// </summary>
        /// <param name="userGame">The UserGame Entity To Map</param>
        /// <returns>A UserGameDTO Representing The Mapped Entity</returns>
        public UserGameDTO ToDTO(UserGame userGame);
    }
}
