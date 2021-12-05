using System;
using System.Collections.Generic;

namespace prototype.UserEritor.Desktop
{
    public interface IResponse
    {
        object Data { get; set; }
        string Description { get; set; }
        DateTime EventTime { get; }
        bool IsValid { get; set; }

        string GetMessage();
    }
    public interface IResponse<T> : IResponse
    {
        T Value { get; set; }
    }
    public interface IPagingResponse : IResponse
    {
        int NavsCount { get; set; }
        int PageCount { get; }
        int PageIndex { get; set; }
        IEnumerable<int> PageNumbers { get; }
        int PageSize { get; set; }
        int StartPageIndex { get; }
        int StopPageIndex { get; }
        int TotalRecordCount { get; set; }

    }
    public interface IPagingResponse<TItem>: IPagingResponse
    {
        IEnumerable<TItem> Value { get; set; }
    }
}