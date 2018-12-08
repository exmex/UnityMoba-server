using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestPlayerDetail
    {
        [Packet("RequestPlayerDetail")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestPlayerDetail"),
                new JProperty("Result", "1"), // 0x0100
                new JProperty("Player",
                    new JObject(
                        new JProperty("Id", 1),
                        new JProperty("Name", SampleData.Player.Name),
                        new JProperty("GuildStatus", 0),
                        new JProperty("Coin", 999999),
                        new JProperty("Exp", 0),
                        new JProperty("Level", SampleData.Player.Level),
                        new JProperty("Gem", 999999),
                        new JProperty("Vip", SampleData.Player.Vip),
                        new JProperty("Avatar", "I11000301"),
                        new JProperty("RunePiece", 999999),
                        new JProperty("GuildCoin", 999999),
                        new JProperty("Voucher", 999999),
                        new JProperty("NextFirstWinTime", DateTime.Now.ToString("o"))
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}