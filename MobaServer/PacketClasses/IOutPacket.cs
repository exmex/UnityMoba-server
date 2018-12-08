using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public interface IOutPacket
    {
        JObject GetContent();
        byte[] GetData();
    }
}