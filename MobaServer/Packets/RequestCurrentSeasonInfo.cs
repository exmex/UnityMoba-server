using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestCurrentSeasonInfo
    {
        [Packet("RequestCurrentSeasonInfo")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestCurrentSeasonInfo"),
                new JProperty("Result", "1"),
                new JProperty("Season", new JObject(
                        new JProperty("End", DateTime.Now.AddYears(2).ToString("o"))
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}