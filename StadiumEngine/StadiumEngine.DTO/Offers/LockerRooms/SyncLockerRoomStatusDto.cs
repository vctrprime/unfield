using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.DTO.Offers.LockerRooms;

/// <summary>
/// Новый статус после сихнронизации
/// </summary>
public sealed class SyncLockerRoomStatusDto
{
    /// <summary>
    /// Новый статус
    /// </summary>
    public LockerRoomStatus Status { get; set; }
}