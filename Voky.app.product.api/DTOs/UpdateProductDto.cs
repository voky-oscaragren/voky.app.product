using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record UpdateProductDto(
    [Required, MinLength(1), MaxLength(200)] string Name,
    [MaxLength(2000)] string? Description,
    [Range(0, double.MaxValue)] decimal Price,
    [Range(0, int.MaxValue)] int Stock
);
