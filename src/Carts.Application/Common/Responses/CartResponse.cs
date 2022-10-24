namespace Carts.Application.Common.Responses;

public class CartResponse
{
    public Guid Id { get; set; }
    public List<CartItemResponse> Items { get; set; } = new();
}
