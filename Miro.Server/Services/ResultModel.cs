using Miro.Server.Interfaces;

namespace Miro.Server.Services
{
    public class ResultModel<TModel> : IResultModel<TModel>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TModel? Data { get; set; }
        public ResultModel(TModel? data)
        {
            IsSuccess = false;
            Message = string.Empty;
            Data = data;
        }
    }
}
