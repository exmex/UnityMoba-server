using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestValidLimitFreeRoleList
    {
        [Packet("RequestValidLimitFreeRoleList")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestValidLimitFreeRoleList"),
                new JProperty("Result", "0") /*, // 1
                    new JProperty("List", new JArray())*/
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}