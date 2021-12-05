

namespace prototype.UserEritor.Desktop
{
    public interface IPagingListService<TPagingRequest,TPagingResponse>
        where TPagingRequest: PagingRequest
        where TPagingResponse : PagingResponse
    {
    }
}
