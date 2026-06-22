using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateQuestionChoiceDto(
    int QuestionId,
    [MaxLength(250)] string NameSwedish,
    [MaxLength(250)] string NameEnglish,
    [MaxLength(40)] string? ProductNumber
);
