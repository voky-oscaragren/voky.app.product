using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateQuestionDto(
    Guid QuestionGroupId,
    [MaxLength(50)] string? QuestionTypeId,
    bool Mandatory,
    [Required, MaxLength(60)] string NameSwedish,
    [Required, MaxLength(60)] string NameEnglish
);
