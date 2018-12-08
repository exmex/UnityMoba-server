using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestAddOnlineStatus
    {
        [Packet("RequestAddOnlineStatus")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestAddOnlineStatus")
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}