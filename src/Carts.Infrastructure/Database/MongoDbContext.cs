using System.Diagnostics.CodeAnalysis;

using Carts.Domain;
using Carts.Infrastructure.OutboxMessages;

using MongoDB.Driver;

namespace Carts.Infrastructure.Database;

[ExcludeFromCodeCoverage]
public sealed class MongoDbContext : IMongoContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connection, string database)
    {
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(connection);
        settings.ClusterConfigurator = cb =>
        {
            //cb.Subscribe<CommandStartedEvent>(e =>
            //{
            //    Console.WriteLine($"{e.Command.ToJson(new JsonWriterSettings { Indent = true })}");
            //});
        };

        MongoClient = new MongoClient(settings);

        _database = MongoClient.GetDatabase(database);
    }

    public IMongoClient MongoClient { get; private set; }

    public IMongoCollection<Cart> Carts =>
        _database.GetCollection<Cart>(typeof(Cart).Name);

    public IMongoCollection<OutboxMessage> OutboxMessages =>
        _database.GetCollection<OutboxMessage>(typeof(OutboxMessage).Name);

    public IMongoCollection<OutboxConsumer> OutboxConsumers =>
        _database.GetCollection<OutboxConsumer>(typeof(OutboxConsumer).Name);
}
