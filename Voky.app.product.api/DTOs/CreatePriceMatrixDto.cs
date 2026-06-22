using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreatePriceMatrixDto(
    [Required, MaxLength(40)] string ProductNr,
    int CurrencyNo,
    [Range(0, double.MaxValue)] decimal EndCustPrice,
    [Range(0, double.MaxValue)] decimal MOQ,
    [Range(0, double.MaxValue)] decimal SupplierNetPrice,
    int CurrenyNr
);
