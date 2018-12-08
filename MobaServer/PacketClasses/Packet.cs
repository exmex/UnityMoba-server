using Newtonsoft.Json.Linq;

namespace MobaServer
{
    public class Packet
    {
        public readonly string Id;
        public readonly Client Client;
        public readonly JObject Content;
        public readonly byte[] Data;
	
        public Packet(string id)
        {
            Id = id;
        }
	
        public Packet(Client client, string id, JObject content, byte[] data)
        {
            Client = client;
            Id = id;
            Content = content;
            Data = data;
        }
    }
}