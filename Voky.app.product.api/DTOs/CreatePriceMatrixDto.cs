using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreatePriceMatrixDto(
    [Required, MaxLength(50)] string ProductNr,
    int SupplierCurrencyId,
    int CurrencyEndPriceId,
    [Range(0, double.MaxValue)] decimal EndCustPrice,
    [Range(0, int.MaxValue)] int? MOQ,
    [Range(0, double.MaxValue)] decimal SupplierNetPrice
);
