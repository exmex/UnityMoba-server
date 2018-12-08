using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestMyselfPreparingGuildDetail
    {
        [Packet("RequestMyselfPreparingGuildDetail")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestMyselfPreparingGuildDetail"),
                new JProperty("Result", "0") /*,
                    new JProperty("Guild", new JArray())
                    */
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}