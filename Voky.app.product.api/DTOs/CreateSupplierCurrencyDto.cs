using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateSupplierCurrencyDto(
    [Required, MaxLength(10)] string Name
);
