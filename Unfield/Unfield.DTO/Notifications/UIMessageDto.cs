using Unfield.Common.Enums.Notifications;

namespace Unfield.DTO.Notifications;

/// <summary>
/// ДТО UI-оповещения
/// </summary>
public sealed class UIMessageDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Тип
    /// </summary>
    public UIMessageType MessageType { get; set; }
    
    /// <summary>
    /// Прочитано
    /// </summary>
    public bool IsRead { get; set; }
    
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTime? DateModified { get; set; }

    /// <summary>
    /// Фрагменты оповещения
    /// </summary>
    public List<UIMessageTextDto> Texts { get; set; } = new List<UIMessageTextDto>();
}

/// <summary>
/// ДТО фрагмента оповещения
/// </summary>
public sealed class UIMessageTextDto
{
    /// <summary>
    /// Индекс
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; set; } = null!;
}