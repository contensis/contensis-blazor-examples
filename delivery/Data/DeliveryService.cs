using System;
using System.Linq;
using System.Threading.Tasks;
using Zengenti.Contensis.Delivery;
using Zengenti.Data;

namespace Delivery.Data;

public class DeliveryService
{
    private readonly ContensisClient _client;

    public DeliveryService(ContensisClient client)
    {
        _client = client;
    }

    public Task<PagedList<Entry>> GetEntries()
    {
        var entries = _client.Entries.List("book", null);
        return Task.FromResult(entries);
    }

    public Task<Node> GetNodes()
    {
        var node = _client.Nodes.GetRoot();

        return Task.FromResult(node);
    }

}
