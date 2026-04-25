using System;

namespace GoodHamburger.Application.Models.ViewModels
{
    public record ProductResponse(
        Guid Id,
        string Name,
        decimal Price,
        string Category
    )
    {
        public static ProductResponse FromEntity(Domain.Entities.Product product)
        {
            return new ProductResponse(
                product.Id,
                product.Name,
                product.Price,
                product.Category.ToString()
            );
        }
    }
}
