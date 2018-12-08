using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestPlayerBountyInfo
    {
        [Packet("RequestPlayerBountyInfo")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestPlayerBountyInfo"),
                new JProperty("Bounties", new JArray(
                        new JObject(
                            new JProperty("Id", 1),
                            new JProperty("Category", 1),
                            new JProperty("LevelLimit", 1)
                        )
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}