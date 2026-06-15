using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateLifecycleDto(
    [Required, MaxLength(100)] string Name
);
