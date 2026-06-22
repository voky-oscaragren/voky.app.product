using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record UpdateProductDto(
    [Required, MaxLength(60)] string Name,
    [Required, MaxLength(100)] string LevArtNrName,
    [MaxLength(40)] string? ArtNrVarianthead,
    [MaxLength(1000)] string? Description,
    [MaxLength(60)] string? ArtNrStartCost,
    int? StartCostAmount,
    int MainSupplierNr,
    int? QuestionGroupId,
    int Moq_customer
);
