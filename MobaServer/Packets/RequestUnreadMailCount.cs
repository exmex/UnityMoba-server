using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestUnreadMailCount
    {
        [Packet("RequestUnreadMailCount")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestUnreadMailCount"),
                new JProperty("Result", "2") /*, // 1
                    new JProperty("UnreadFriendMailCount", "0"),
                    new JProperty("UnreadSystemMailCount", "0")*/
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}