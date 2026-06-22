using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateQuestionGroupDto(
    [MaxLength(250)] string Name
);
