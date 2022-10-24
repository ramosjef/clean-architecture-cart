namespace Carts.Application.Common.Contracts;

public interface ISuccess<in Tin>
{
    void Success(Tin success);
}
