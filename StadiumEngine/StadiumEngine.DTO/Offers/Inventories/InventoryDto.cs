using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.DTO.Offers.Inventories;

public class InventoryDto : BaseEntityDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Изображения
    /// </summary>
    public List<string> Images { get; set; }

    /// <summary>
    ///     Цена
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Валюта
    /// </summary>
    public Currency Currency { get; set; }

    /// <summary>
    ///     Количество
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    ///     Список видов спорта
    /// </summary>
    public List<SportKind> SportKinds { get; set; }

    /// <summary>
    ///     Активность
    /// </summary>
    public bool IsActive { get; set; }
}