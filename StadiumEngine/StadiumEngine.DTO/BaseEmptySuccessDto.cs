namespace StadiumEngine.DTO;

/// <summary>
///     Базовое пустое ДТО ответа
/// </summary>
public class BaseEmptySuccessDto
{
    /// <summary>
    ///     Сообщение
    /// </summary>
    public string Message { get; set; } = "Успешно!";
}