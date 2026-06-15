using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateCurrencyEndPriceDto(
    [Required, MaxLength(10)] string Name
);
