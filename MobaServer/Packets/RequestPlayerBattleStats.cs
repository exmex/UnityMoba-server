using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestPlayerBattleStats
    {
        [Packet("RequestPlayerBattleStats")]
        public static void Handle(Packet packet)
        {
            var responseContent = new JObject(
                new JProperty("Type", "RequestPlayerBattleStats"),
                new JProperty("Result", "1"),
                new JProperty("Stats", new JObject(
                        new JProperty("PlayerName", SampleData.Player.Name),
                        new JProperty("PlayerLevel", SampleData.Player.Level),
                        new JProperty("E", new JObject(
                            new JProperty("BattleCount", 50),
                            new JProperty("WinnerCount", 10),
                            new JProperty("HonorInUse", 0),
                            new JProperty("Credit", 0)
                        )),
                        new JProperty("T", new JObject(
                            new JProperty("WinnerMvp", 0),
                            new JProperty("LoserMvp", 0),
                            new JProperty("Legendary", 0),
                            new JProperty("PentaKill", 0),
                            new JProperty("QuadraKill", 0),
                            new JProperty("TripleKill", 0)
                        )),
                        new JProperty("PlayerIcon", SampleData.Player.Avatar),
                        new JProperty("PlayerIconFrameId", SampleData.Player.FrameId),
                        new JProperty("RoleCount", 1),
                        new JProperty("SkinCount", 1),
                        new JProperty("GuildName", SampleData.Player.GuildName),
                        new JProperty("GuildPosition", SampleData.Player.GuildPosition),
                        new JProperty("TypeBattles",
                            new JArray(
                                new JObject(
                                    new JProperty("BattleType", 1), // 1, 3, 5, 7
                                    new JProperty("BattleCount", 0),
                                    new JProperty("WinnerCount", 0)
                                )
                            )
                        ),
                        new JProperty("TypeHonors",
                            new JArray(
                                new JObject(
                                    new JProperty("HonorId", 1),
                                    new JProperty("Count", 1),
                                    new JProperty("HonorType", 1)
                                )
                            )
                        ),
                        new JProperty("UsualRoles",
                            new JArray(
                                new JObject(
                                    new JProperty("RoleId", 1),
                                    new JProperty("BattleCount", 0),
                                    new JProperty("WinnerCount", 0),
                                    new JProperty("ProficiencyId", 1)
                                )
                            )
                        )
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}