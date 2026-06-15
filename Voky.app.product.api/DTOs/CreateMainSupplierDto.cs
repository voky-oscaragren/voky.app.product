using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateMainSupplierDto(
    [Required, MaxLength(200)] string Name,
    [MaxLength(200)] string? Email,
    [MaxLength(200)] string? BankAccountGiro
);
