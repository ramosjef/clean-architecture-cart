namespace Carts.Application.Common.Contracts;

public interface INotFound<T>
{
    void NotFound(T response);
}
