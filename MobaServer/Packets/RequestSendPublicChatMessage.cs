using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestSendPublicChatMessage
    {
        [Packet("RequestSendPublicChatMessage")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestSendPublicChatMessage"),
                new JProperty("Result", "1") // 0, 1, 0x0901 (Send interval is too short)
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            
            /*
  local sender = chatmes.Sender
  local sendtime = chatmes.SendTime
  local mestype = tonumber(chatmes.MessageType)
  local receiverId = tonumber(chatmes.ReceiverId)
  local data = {}
  data.ReceiverId = receiverId
  --频道
  data.Channel = tonumber(chatmes.MessageChannel)
             */
            // TODO: Send to all
            var responseContent2 = new JObject(
                new JProperty("Type", "NotifyChatMessage"),
                new JProperty("ChatMessage",
                    new JObject(
                        new JProperty(
                            "Sender", SampleData.Player.Serialize()
                        ),
                        new JProperty(
                            "SendTime", DateTime.Now.ToString("o")
                        ),
                        new JProperty(
                            "MessageType", packet.Content["MessageType"] // 1 = System, 4 = SmallHorn, 5 = BigHorn
                        ),
                        new JProperty(
                            "ReceiverId", "" 
                        ),
                        new JProperty(
                            "MessageChannel", "1" 
                        ),
                        new JProperty(
                            "Message", packet.Content["Message"] 
                        )
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent2.ToString(), new byte[0]);
        }
    }
}