using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestMailList
    {
        [Packet("RequestMailList")]
        public static void Handle(Packet packet)
        {
            /*
Type": "RequestMailList",
"Category": 1.0,
"BeginIndex": 0.0,
"Length": 5.0
             */
            var responseContent = new JObject(
                new JProperty("Type", "RequestMailList"),
                new JProperty("Result", "1"), // 3077
                new JProperty("MailList", new JArray(
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}