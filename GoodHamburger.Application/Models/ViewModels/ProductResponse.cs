using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Models.ViewModels
{
    public record ProductResponse(
        Guid Id,
        string Name,
        string Code,
        decimal Price,
        string Type
    );
}
