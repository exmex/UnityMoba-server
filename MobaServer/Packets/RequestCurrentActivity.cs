using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestCurrentActivity
    {
        [Packet("RequestCurrentActivity")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestCurrentActivity"),
                new JProperty("Result", "0")
                /*
                new JProperty("Activities", new JArray(
                ActivityQuests
                        new JObject(
                            new JProperty("Id", 1)
                        )
                    )
                )
                */
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}