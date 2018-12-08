using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestCreateParty
    {
        [Packet("RequestCreateParty")]
        public static void Handle(Packet packet)
        {
            /*
JObject.New(JProperty.New("Type","RequestCreateParty"),
  JProperty.New("BMainType", mainType),
  JProperty.New("BSubType", subType),
  JProperty.New("BDifficulty", difficulty))
             */
            var responseContent = new JObject(
                new JProperty("Type", "RequestCreateParty"),
                new JProperty("Result", "1")
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);


            /*var responseContent2 = new JObject(
                new JProperty("Type", "NotifyPartyChange"),
                new JProperty("MMaxCount", "1"),
                new JProperty("BMainType", packet.Content["BMainType"]), // 52
                new JProperty("BSubType", packet.Content["BSubType"]), // 52
                new JProperty("AllowGradeCategorys", ""),
                new JProperty("Members", new JArray(
                        Program.BasePlayer
                    )
                ),
                new JProperty("Leader", Program.BasePlayer)
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent2.ToString(), new byte[0]);*/

            var responseContent2 = new JObject(
                new JProperty("Type", "NotifyRoomChange"),
                new JProperty("RoomInfo", new JObject(
                        new JProperty("Id", 1),
                        new JProperty("Owner", SampleData.Player.Serialize()),
                        new JProperty("MMaxCount", "1"),
                        new JProperty("BMainType", packet.Content["BMainType"]), // 52
                        new JProperty("BSubType", packet.Content["BSubType"]), // 52
                        new JProperty("Members", new JArray(
                                SampleData.Player.Serialize()
                            )
                        )
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent2.ToString(), new byte[0]);
        }
    }
}