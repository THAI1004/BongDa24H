namespace Backend.Dtos;

public class CreatePromotionDto
{
    public string DiscountCode { get; set; } = null!;

    public int DiscountPercent { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public int? ClusterId { get; set; }

}