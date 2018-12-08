using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestPlayerDeck
    {
        [Packet("RequestPlayerDeck")]
        public static void Handle(Packet packet)
        {
//Category
            var responseContent = new JObject(
                new JProperty("Type", "RequestPlayerDeck"),
                new JProperty("Result", "1"),
                new JProperty("Roles",
                    new JArray(
                        new JObject(
                            new JProperty("Id", 1),
                            new JProperty("RoleId", 1),
                            new JProperty("IsOwn", true)
                        )
                    )
                ),
                new JProperty("Skins",
                    new JArray(
                    )
                ),
                new JProperty("RunePages",
                    new JArray(
                    )
                ),
                new JProperty("RuneSlots",
                    new JArray(
                    )
                ),
                new JProperty("Runes",
                    new JArray(
                    )
                ),
                new JProperty("Items",
                    new JArray(
                    )
                ),
                new JProperty("PlayerSkills",
                    new JArray(
                    )
                ),
                new JProperty("Seasons",
                    new JArray(
                    )
                ),
                new JProperty("Grade",
                    new JArray(
                    )
                ),
                new JProperty("PlayerShop",
                    new JObject(
                        new JProperty("NextFreeRollRuneOnceByGemTime", DateTime.Now.ToString("o"))
                    )
                ),
                new JProperty("PlayerGrowUps",
                    new JArray(
                    )
                ),
                new JProperty("PlayerGrowUpDeck",
                    new JObject(
                        new JProperty("DrewLevelRewards",
                            new JArray(
                            )
                        )
                    )
                ),
                new JProperty("PlayerLevelQuests",
                    new JArray(
                    )
                ),
                new JProperty("PlayerGain",
                    new JArray(
                    )
                ),
                new JProperty("PlayerAvatarFrames",
                    new JArray(
                    )
                ),
                new JProperty("PlayerAchievementInfo",
                    new JArray(
                    )
                ),
                new JProperty("PlayerAchievements",
                    new JArray(
                    )
                ),
                new JProperty("PlayerAchievementProgresses",
                    new JArray(
                    )
                ),
                new JProperty("PlayerActivity",
                    new JArray(
                    )
                ),
                new JProperty("PlayerActivityQuests",
                    new JArray(
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}