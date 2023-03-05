using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.DTO.Offers.LockerRooms;

/// <summary>
/// ДТО раздевалки
/// </summary>
public class LockerRoomDto : BaseEntityDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///  Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///  Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///  Пол
    /// </summary>
    public LockerRoomGender Gender { get; set; }

    /// <summary>
    ///  Активность
    /// </summary>
    public bool IsActive { get; set; }
}