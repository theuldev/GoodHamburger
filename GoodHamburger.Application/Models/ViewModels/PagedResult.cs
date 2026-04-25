using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Models.ViewModels
{
    public class PagedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}
