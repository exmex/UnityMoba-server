using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestVersion
    {
        [Packet("RequestVersion")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestVersion"),
                new JProperty("Result", "1"),
                new JProperty("DataVersion", "1.0.0")
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}