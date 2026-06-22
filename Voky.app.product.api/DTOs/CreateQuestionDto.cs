using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.DTOs;

public record CreateQuestionDto(
    int QuestionGroupId,
    int? QuestionTypeId,
    byte Mandatory,
    [MaxLength(250)] string NameSwedish,
    [MaxLength(250)] string NameEnglish
);
