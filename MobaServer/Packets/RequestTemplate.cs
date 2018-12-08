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
                new JProperty("Items", new JArray(
                    new JObject(
                        new JProperty("Id", "1"),
                        new JProperty("Name", "Test"),
                        new JProperty("Icon", "1"),
                        new JProperty("Quality", 1),
                        new JProperty("Type", 1), // 7,8,13,14,15,17
                        new JProperty("Desc", "Testing"),
                        new JProperty("Amount", 1),
                        new JProperty("Param",
                            new JArray()
                        )
                    )
                )),
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
                            new JProperty("Rewards", new JArray(
                                    new JObject(
                                        new JProperty("Id", 1),
                                        new JProperty("Amount", 1)
                                    )
                                )
                            )
                        ),
                        new JObject(
                            new JProperty("Level", 2),
                            new JProperty("NextExp", 20000),
                            new JProperty("IsPowerful", true),
                            new JProperty("Rewards", new JArray(
                                    new JObject(
                                        new JProperty("Id", 1),
                                        new JProperty("Amount", 1)
                                    )
                                )
                            )
                        )
                    )
                ),
                new JProperty("Grades", new JArray()),
                new JProperty("Seasons", new JArray()),
                new JProperty("QuickMessages", new JArray()),
                new JProperty("MailInfos", new JArray()),
                new JProperty("BattleHonors", new JArray()),
                new JProperty("GrowUps", new JArray()),
                new JProperty("GrowUpChests", new JArray(
                    new JObject(
                        new JProperty("Id", 1),
                        new JProperty("Type", 1), // 1 = Day, 2 = Week
                        new JProperty("ActivePoint", 1),
                        new JProperty("RewardId", 1)
                    ),
                    new JObject(
                        new JProperty("Id", 2),
                        new JProperty("Type", 2), // 1 = Day, 2 = Week
                        new JProperty("ActivePoint", 1),
                        new JProperty("RewardId", 1)
                    ),
                    new JObject(
                        new JProperty("Id", 3),
                        new JProperty("Type", 2), // 1 = Day, 2 = Week
                        new JProperty("ActivePoint", 1),
                        new JProperty("RewardId", 1)
                    ),
                    new JObject(
                        new JProperty("Id", 4),
                        new JProperty("Type", 2), // 1 = Day, 2 = Week
                        new JProperty("ActivePoint", 1),
                        new JProperty("RewardId", 1)
                    ),
                    new JObject(
                        new JProperty("Id", 5),
                        new JProperty("Type", 2), // 1 = Day, 2 = Week
                        new JProperty("ActivePoint", 1),
                        new JProperty("RewardId", 1)
                    )
                )),
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
                new JProperty("EntModes", // Entertainment Modes
                    new JArray(
                        new JObject(
                            new JProperty("Id", 1),
                            new JProperty("Category", 1),
                            new JProperty("StartEnd", new JArray(
                                    (object) new JArray(
                                        // It ignores day and month, and only looks at weekday (*)
                                        // when it's * it is only dependend on the hour. If its a proper weekday (1-7) it looks if it is that day
                                        DateTime.Now.AddHours(-1).ToString("ss mm HH dd MM *"),
                                        DateTime.Now.AddHours(1).ToString("ss mm HH dd MM *")
                                    )
                                )
                            ),
                            new JProperty("OpenDesc", "Test")
                        )
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