using Newtonsoft.Json.Linq;

namespace MobaServer
{
    public class Role : ISerializable
    {
        public string Id;
        public string Name;
        public string Portrait;
        public string Icon;
        public string Desc;
        
        public JObject Serialize()
        {
            return new JObject(
                new JProperty("Id", Id),
                new JProperty("Name", Name),
                new JProperty("Portrait", Portrait),
                new JProperty("Icon", Icon),
                new JProperty("Desc", Desc)
            );
        }

        public void Deserialize(JObject content)
        {
            throw new System.NotImplementedException();
        }
    }
}