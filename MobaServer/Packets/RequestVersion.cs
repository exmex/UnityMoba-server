using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestVersion
    {
        [Packet("RequestVersion")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestVersion"),
                new JProperty("Result", "1"),
                new JProperty("DataVersion",
                    DateTime.Now.ToString("o")) // This will force the client to send the "RequestTemplate" packet
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}