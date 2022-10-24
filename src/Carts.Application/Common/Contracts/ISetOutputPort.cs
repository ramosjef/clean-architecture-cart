namespace Carts.Application.Common.Contracts;

public interface ISetOutputPort<T> where T : class
{
    void SetOutputPort(T outputPort);
}
