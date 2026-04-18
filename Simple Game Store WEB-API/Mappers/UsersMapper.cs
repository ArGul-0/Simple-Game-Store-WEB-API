using Simple_Game_Store_WEB_API.DTOs.Users;
using Simple_Game_Store_WEB_API.Entities;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public class UsersMapper : IUsersMapper
    {
        public UserGameDTO ToDTO(UserGame userGame)
        {
            return new UserGameDTO(
                Game: userGame.Game,
                PurchasedAt: userGame.PurchasedAt
                );
        }
    }
}
