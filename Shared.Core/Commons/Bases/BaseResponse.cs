namespace Shared.Core.Commons.Bases
{
    public class BaseResponse<T> : BaseMessageResponse
    {
        public T? Data { get; set; }
        public int? TotalRecords { get; set; }
        public IEnumerable<BaseError>? Errors { get; set; }
    }
}
