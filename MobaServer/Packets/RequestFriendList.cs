using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestFriendList
    {
        [Packet("RequestFriendList")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestFriendList"),
                new JProperty("Result", "1"),
                new JProperty("FriendList",
                    new JArray(
                        /*new JObject(
                            new JProperty("PlayerId", 1)
                        )*/
                    )
                ),
                new JProperty("ForbidList",
                    new JArray()
                ),
                new JProperty("FriendCandidateList",
                    new JArray()
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}