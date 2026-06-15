using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record UpdateProductDto(
    [Required, MaxLength(50)] string ProductNr,
    [Required, MaxLength(200)] string Name,
    [MaxLength(200)] string? LevArtNrName,
    [MaxLength(50)] string? ArtNrVarianthead,
    [MaxLength(2000)] string? Description,
    bool SendToOpti,
    [MaxLength(50)] string? ArtNrStartCost,
    [Range(0, double.MaxValue)] decimal? StartCostAmount,
    [MaxLength(50)] string? MainSupplierNr,
    int? LifecycleId,
    int? QuestionGroupId
);
