using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestReconnectStage
    {
        [Packet("RequestReconnectStage")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestReconnectStage"),
                new JProperty("Result", "0")
                /*
                new JProperty("Stage", "")
                new JProperty("PartyInfo", "")
                new JProperty("Content", "")
                */
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}