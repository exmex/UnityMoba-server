using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    /// <summary>
    /// Heartbeat Packet
    /// </summary>
    public class Beat
    {
        public class InBeatPacket
        {
            public DateTime ClientTime;
            
            public InBeatPacket(Packet packet)
            {
                var request = packet.Content;
                
                if(request.ContainsKey("TC"))
                    ClientTime = DateTime.Parse(request["TC"].ToString());
            }
        }

        public class OutBeatPacket : IOutPacket
        {
            public DateTime ClientTime;
            public DateTime ServerTime;
            
            public override JObject GetContent()
            {
                var responseContent = new JObject(
                    new JProperty("Type", "Beat"),
                    new JProperty("TC", ClientTime.ToString("o")),
                    new JProperty("TS", ServerTime.ToString("o"))
                );
                return responseContent;
            }
        }
        
        [Packet("Beat")]
        public static void Handle(Packet packet)
        {
            var beat = new InBeatPacket(packet);

            var beatAck = new OutBeatPacket {ClientTime = beat.ClientTime, ServerTime = DateTime.Now};
            packet.Client.SendBody(TGNetService.NetServiceChannel.Secondary, beatAck.ToString(), beatAck.GetData());
        }
    }
}