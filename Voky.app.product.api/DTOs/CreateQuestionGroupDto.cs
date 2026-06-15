using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateQuestionGroupDto(
    [Required, MaxLength(200)] string Name
);
