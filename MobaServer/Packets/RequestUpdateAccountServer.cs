using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestUpdateAccountServer
    {
        [Packet("RequestUpdateAccountServer")]
        public static void Handle(Packet packet)
        {
            // ServerId
            var responseContent = new JObject(
                new JProperty("Type", "RequestUpdateAccountServer")
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}