using Carts.Domain;
using Carts.Infrastructure.OutboxMessages;

using MongoDB.Driver;

namespace Carts.Infrastructure.Database;

public interface IMongoContext
{
    IMongoClient MongoClient { get; }
    IMongoCollection<Cart> Carts { get; }
    IMongoCollection<OutboxMessage> OutboxMessages { get; }
    IMongoCollection<OutboxConsumer> OutboxConsumers { get; }
}
