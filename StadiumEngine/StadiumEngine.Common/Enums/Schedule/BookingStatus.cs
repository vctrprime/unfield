namespace StadiumEngine.Common.Enums.Schedule;

public enum BookingStatus
{
    /// <summary>
    /// Активна
    /// </summary>
    Active = 1,
    
    /// <summary>
    /// Активная еженедельная
    /// </summary>
    WeeklyActive,
    
    /// <summary>
    /// Активный элемент еженедельной
    /// </summary>
    WeeklyItemActive,
    
    /// <summary>
    /// Завершено
    /// </summary>
    Finished,
    
    /// <summary>
    /// Еженедельное завершено
    /// </summary>
    WeeklyFinished,
    
    /// <summary>
    /// Элемент еженедельного завершен
    /// </summary>
    WeeklyItemFinished
}