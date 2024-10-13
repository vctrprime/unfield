using Unfield.Common.Enums.Offers;

namespace Unfield.DTO.Offers.LockerRooms;

/// <summary>
///     ДТО раздевалки
/// </summary>
public class LockerRoomDto : BaseEntityDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Пол
    /// </summary>
    public LockerRoomGender Gender { get; set; }

    /// <summary>
    ///     Активность
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Статус
    /// </summary>
    public LockerRoomStatus Status { get; set; }
}