using Newtonsoft.Json.Linq;

namespace MobaServer
{
    public interface ISerializable
    {
        JObject Serialize();
        void Deserialize(JObject content);
    }
}