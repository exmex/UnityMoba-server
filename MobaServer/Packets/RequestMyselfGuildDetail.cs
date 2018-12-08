using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestMyselfGuildDetail
    {
        [Packet("RequestMyselfGuildDetail")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestMyselfGuildDetail"),
                new JProperty("Result", "0") /*,
                    new JProperty("Guild", new JArray())
                    */
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}