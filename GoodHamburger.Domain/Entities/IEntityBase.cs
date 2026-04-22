using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Entities
{
    public interface IEntityBase
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime UpdateAt { get; }
    }
}
