using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestServerRanks
    {
        [Packet("RequestServerRanks")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestServerRanks"),
                new JProperty("Result", "0") /*, //1
                    new JProperty("Ranks", )
                    new JProperty("Self", )
                    */
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}