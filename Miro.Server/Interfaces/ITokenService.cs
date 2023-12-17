namespace Miro.Server.Interfaces
{
    public interface ITokenService<T> where T : class
    {
        Task<T> GetByTokenAsync(string tokeb);

        Task UpdateTokenAsync(T user, string newToken);

        string GenerateToken(T user);
    }
}
