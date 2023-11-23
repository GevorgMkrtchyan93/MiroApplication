using Miro.Server.Entities;
using Miro.Server.Services;

namespace Miro.Server.Interfaces
{
    public interface IBoardManager
    {
        Task<ResultModel<Board>> RegisterBoard(int userId,int boardId);

        Task<ResultModel<Board>> ExitBoard(int userId, int boardId);
    }
}
