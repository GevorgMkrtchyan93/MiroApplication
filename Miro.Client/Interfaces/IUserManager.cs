using Miro.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miro.Client.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUserById(int id);

        Task<User> GetUserByTokenId(string token);

        Task<IEnumerable<User>> GetAllUser();

        Task<User> UpdateUser(string token);
    }
}
