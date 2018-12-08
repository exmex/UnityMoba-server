using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestMyselfApplyingGuilds
    {
        [Packet("RequestMyselfApplyingGuilds")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestMyselfApplyingGuilds"),
                new JProperty("Result", "0") /*,
                    new JProperty("ApplyingGuilds", new JArray())
                    */
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}