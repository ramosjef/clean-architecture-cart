namespace Carts.Application.Common.Contracts;

public interface IInvalidRequest<in T>
{
    void Invalid(T applicationError);
}
