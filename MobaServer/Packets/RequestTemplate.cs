using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestTemplate
    {
        [Packet("RequestTemplate")]
        public static void Handle(Packet packet)
        {
            /*
JProperty.New("Category",0xffffffffffff))
             */
            var responseContent = new JObject(
                new JProperty("Type", "RequestTemplate"),
                new JProperty("Result", "1"),
                new JProperty("Roles", new JArray(
                    SampleData.Role.Serialize()
                )),
                new JProperty("RoleProficiencys", new JArray()),
                new JProperty("Equips", new JArray()),
                new JProperty("Items", new JArray()),
                new JProperty("Skills", new JArray()),
                new JProperty("SkillBehaviours", new JArray()),
                new JProperty("Runes", new JArray()),
                new JProperty("RunePages", new JArray()),
                new JProperty("RuneSlots", new JArray()),
                new JProperty("Dramas", new JArray()),
                new JProperty("Skins", new JArray()),
                new JProperty("Shops", new JArray()),
                new JProperty("PartShops", new JArray()),
                new JProperty("ShopNews", new JArray()),
                new JProperty("ShopHots", new JArray()),
                new JProperty("ShopPosts", new JArray()),
                new JProperty("ShopDepreciations", new JArray()),
                new JProperty("ShopTreasures", new JArray()),
                new JProperty("ShopTreasureChests", new JArray()),
                new JProperty("Creatures", new JArray()),
                new JProperty("CreatureGroups", new JArray()),
                new JProperty("PVPLevels", new JArray()),
                new JProperty("PVPLevelUps", new JArray()),
                new JProperty("PVPMalls", new JArray()),
                new JProperty("PVPRoleGrows", new JArray()),
                new JProperty("Source", new JArray()),
                new JProperty("PlayerSkills", new JArray()),
                new JProperty("GodEquipConfigs", new JArray()),
                new JProperty("PVPKillStreak", new JArray()),
                new JProperty("PlayerLevelUps", new JArray(
                        new JObject(
                            new JProperty("Level", 1),
                            new JProperty("NextExp", 10000),
                            new JProperty("IsPowerful", true),
                            new JProperty("Rewards", new JArray())
                        ),
                        new JObject(
                            new JProperty("Level", 2),
                            new JProperty("NextExp", 20000),
                            new JProperty("IsPowerful", true),
                            new JProperty("Rewards", new JArray())
                        )
                    )
                ),
                new JProperty("Grades", new JArray()),
                new JProperty("Seasons", new JArray()),
                new JProperty("QuickMessages", new JArray()),
                new JProperty("MailInfos", new JArray()),
                new JProperty("BattleHonors", new JArray()),
                new JProperty("GrowUps", new JArray()),
                new JProperty("GrowUpChests", new JArray()),
                new JProperty("AvatarFrames", new JArray()),
                new JProperty("Bounties", new JArray(
                        new JObject(
                            new JProperty("Id", 1),
                            new JProperty("Category", 1),
                            new JProperty("LevelLimit", 1)
                        )
                    )
                ),
                new JProperty("DropGroups", new JArray()),
                new JProperty("EntModes", new JObject(
                        new JProperty("StartEnd", new JObject(
                                new JProperty("Start", DateTime.Now.ToString("o")),
                                new JProperty("End", DateTime.Now.AddMinutes(20).ToString("o"))
                            )
                        ),
                        new JProperty("OpenDesc", "Test")
                    )
                ),
                new JProperty("GuildIcons", new JArray()),
                new JProperty("GuildLevels", new JArray()),
                new JProperty("GuildMemberLimits", new JArray()),
                new JProperty("GuildPermissions", new JArray()),
                new JProperty("GuildShopRefreshs", new JArray()),
                new JProperty("GuildStarLevels", new JArray()),
                new JProperty("GuildWeeklyRanks", new JArray()),
                new JProperty("LevelQuests", new JArray()),
                new JProperty("FuncLocks", new JArray(
                        new JObject(
                            new JProperty("Type", 5),
                            new JProperty("UnlockLevel", 1)
                        )
                    )
                ),
                new JProperty("GrowUpGuides", new JArray()),
                new JProperty("Achievements", new JArray()),
                new JProperty("AchievementLevelUps", new JArray()),
                new JProperty("Announcements", new JArray()),
                new JProperty("SignIns", new JArray())
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}