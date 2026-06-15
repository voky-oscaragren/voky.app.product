using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateQuestionChoiceDto(
    Guid QuestionId,
    [Required, MaxLength(60)] string NameSwedish,
    [Required, MaxLength(60)] string NameEnglish,
    [MaxLength(50)] string? ProductNumber
);
