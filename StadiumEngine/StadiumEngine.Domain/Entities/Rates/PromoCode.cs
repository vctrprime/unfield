using System.ComponentModel.DataAnnotations.Schema;
using StadiumEngine.Common.Enums.Rates;

namespace StadiumEngine.Domain.Entities.Rates;

[Table( "promo_code", Schema = "rates" )]
public class PromoCode : BaseUserEntity
{
    [Column( "code" )]
    public string Code { get; set; }
    
    [Column( "type" )]
    public PromoCodeType Type { get; set; }
    
    [Column( "value" )]
    public decimal Value { get; set; }
    
    [Column( "tariff_id" )]
    public int TariffId { get; set; }

    [ForeignKey( "TariffId" )]
    public virtual Tariff Tariff { get; set; }
    
    [NotMapped]
    public new string Description { get; set; }
    [NotMapped]
    public new string Name { get; set; }
}