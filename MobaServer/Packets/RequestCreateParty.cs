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

            // MainType == 3 = Solo Bots
            // MainType == 1 = PvP
            /*
             * PlayerCount = 5 means 5 on each team
             * 
             * MainType = 1 = PvP
             *     SubType = 10 = Solo
             *     SubType = 30 = 3v3
             *     SubType = 50 = 5v5
             *     SubType = 51 = Arena 5v5
             * MainType = 2 = Entertainment
             *     SubType = 80 = ?
             * MainType = 3 = Computer
             *     SubType = 10 = Solo
             *     SubType = 30 = 3v3
             *     SubType = 50 = 5v5
             *     SubType = 51 = 5v5 Arena
             */
            var responseContent2 = new JObject(
                new JProperty("Type", "NotifyPartyChange"),
                new JProperty("MMaxCount", 1),
                new JProperty("BMainType", packet.Content["BMainType"]), // 52
                new JProperty("BSubType", packet.Content["BSubType"]), // 52
                new JProperty("AllowGradeCategorys", ""),
                new JProperty("Members", new JArray(
                        SampleData.Player.Serialize()
                    )
                ),
                new JProperty("Leader", SampleData.Player.Serialize())
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent2.ToString(), new byte[0]);

            /*var responseContent2 = new JObject(
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
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent2.ToString(), new byte[0]);*/
        }
    }
}