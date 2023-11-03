namespace Miro.Server.Interfaces
{
    public interface IResultModel<TEntity>
    {
        bool IsSuccess { get; set; }
        string? Message { get; set; }
        TEntity? Data { get; set; }
    }
}
