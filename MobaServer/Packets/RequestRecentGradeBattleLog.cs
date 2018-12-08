using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestRecentGradeBattleLog
    {
        [Packet("RequestRecentGradeBattleLog")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestRecentGradeBattleLog"),
                new JProperty("Result", "1"),
                new JProperty("BattleLogs", new JArray())
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}