using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public abstract class IOutPacket
    {
        public abstract JObject GetContent();

        public virtual byte[] GetData()
        {
            return new byte[0];
        }

        public override string ToString()
        {
            return GetContent().ToString();
        }
    }
}