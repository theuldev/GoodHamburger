using System;
using System.Collections.Generic;
using System.Text;

namespace MovimentAPI.Application.ViewModels
{
    public record PagedResponse<T>(
      int Page,
      int PageSize,
      int Total,
      IEnumerable<T> Data
  );
}
