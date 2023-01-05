namespace StadiumEngine.DTO.Accounts;

/// <summary>
/// ДТО разрешения
/// </summary>
public class PermissionDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Порядок сортировки
    /// </summary>
    public int SortValue { get; set; }
    
    /// <summary>
    /// Название группы
    /// </summary>
    public string GroupName { get; set; }
    
    /// <summary>
    /// Порядок сортировки группы
    /// </summary>
    public int GroupSortValue { get; set; }
    
    /// <summary>
    /// Связан с ролью, для которой был запрос
    /// </summary>
    public bool IsRoleBound { get; set; }
}