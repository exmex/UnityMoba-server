using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobaServer
{
    public class Client
    {
        private readonly Socket _socket;

        public Client(Socket handler)
        {
            _socket = handler;

            Receive();
        }

        private void HandlePkt(TGNetService.NetServiceChannel channel, JObject response, byte[] data)
        {
            if (response["Type"].ToString() != "Beat")
            {
                if (Program.Parsers.ContainsKey(response["Type"].ToString()))
                {
                    Console.WriteLine($"Handling Packet ({channel}): {response["Type"]}");
                    Program.Parsers[response["Type"].ToString()](new Packet(this, response["Type"].ToString(), response,
                        data));
                }
                else if (!Program.Parsers.ContainsKey(response["Type"].ToString()))
                {
                    Console.WriteLine($"Unknown Packet ({channel}): {response["Type"]}");
                }
            }

            #region Old

#if false
            if (channel == TGNetService.NetServiceChannel.Primary && response["Type"].ToString() == "Beat")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "Beat"),
                    new JProperty("TC", response["TC"] /*DateTime.Now.ToString("o")*/),
                    new JProperty("TS", DateTime.Now.ToString("o"))
                );

                SendBody(TGNetService.NetServiceChannel.Secondary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "Login")
            {
                /*
 loginRequest.Content = JObject.New(JProperty.New("Type","Login"),
JProperty.New("UserName",username),
JProperty.New("Password",password),
JProperty.New("DeviceType","All"),
JProperty.New("DeviceId","6969696969696969696"))

 if e.Type == "Login" then  
    local result = tonumber(e.Content:get_Item("Result"):ToString())
    if result == 1 then      
      self.accountInfo.AccountId = tonumber(e.Content:get_Item("AccountId"):ToString())
      self.accountInfo.Session = e.Content:get_Item("Session"):ToString()

      local accountId = tonumber(e.Content:get_Item("AccountId"):ToString())
      local session = e.Content:get_Item("Session"):ToString()
      
      if self.reLoginCallBack ~= nil then
        self.reLoginCallBack(self.reLoginCaller,accountId, session)
      end  
      return true
    end 
  end
                 */
                var responseContent = new JObject(
                    new JProperty("Type", "Login"),
                    new JProperty("Result", "1"),
                    new JProperty("AccountId", "1"),
                    new JProperty("Session", "1")
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestServerList")
            {
                /*
if e.Type == "RequestServerList" then
    local servers = json.decode(e.Content:get_Item("Servers"):ToString())
    self.Servers = {}
    for k,v in pairs(servers) do
      self.Servers[tostring(servers[k].Id)] = servers[k]
    end
    return true
  end
  
if e.Type == "RequestServerList" then
      --print("获取服务器信息 成功")
      local result = tonumber(e.Content:get_Item("Result"):ToString())      
      if result == 1 then
        --上次登陆
        self.lastlogon = json.decode(e.Content:get_Item("LastLogon"):ToString())
        if self.lastlogon.Addr=="" then
          --print("self.lastlogon22222")
          self.lastlogon = nil
        end
        
        --曾经登陆
        local everservers = json.decode(e.Content:get_Item("EverLogon"):ToString())
        self.everLogon = {}
        if everservers~=nil then
          for i=1,#everservers do
          self.everLogon[i] = everservers[i]
        end
        end
        --推荐登录
        local suggservers = json.decode(e.Content:get_Item("Suggested"):ToString())
        self.suggestedserver = {}
        
      if suggservers ~= nil then
        for k,v in pairs(suggservers) do  
          self.suggestedserver[k] = v
        end
      end
        --所有服务器
        local allservers = json.decode(e.Content:get_Item("All"):ToString())
        self.allserver = {}

        for k,v in pairs(allservers) do  
          self.allserver[k] = v
        end
        --初始化
        self:Init()
      else 
        Debugger.LogError("获取服务器列表失败") 
    end 
    return true
  end 
                 */
                var responseContent = new JObject(
                    new JProperty("Type", "RequestServerList"),
                    new JProperty("Result", 1),
                    new JProperty("LastLogon", new JObject(
                        new JProperty("Addr", "")
                    )),
                    new JProperty("EverLogon", new JArray()),
                    new JProperty("Suggested", new JArray(
                        new JObject(
                            new JProperty("Id", "1"),
                            new JProperty("Name", "Local"),
                            new JProperty("Prefix", "T"),
                            new JProperty("Status", "2"),
                            new JProperty("Addr", "127.0.0.1"),
                            new JProperty("Port", 25001)
                        )
                    )),
                    new JProperty("All", new JArray(
                            new JObject(
                                new JProperty("Id", "1"),
                                new JProperty("Name", "Local"),
                                new JProperty("Prefix", "T"),
                                new JProperty("Status", "2"),
                                new JProperty("Addr", "127.0.0.1"),
                                new JProperty("Port", 25001)
                            )
                        )
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestUpdateAccountServer")
            {
                // ServerId
                var responseContent = new JObject(
                    new JProperty("Type", "RequestUpdateAccountServer")
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "Auth")
            {
                // AccountId
                // SessionKey
                var responseContent = new JObject(
                    new JProperty("Type", "Auth"),
                    new JProperty("Result", "1") // 0x0303 = ReLoginToGetNewData
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestPlayerDetail")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestPlayerDetail"),
                    new JProperty("Result", "1"), // 0x0100
                    new JProperty("Player",
                        new JObject(
                            new JProperty("Id", 1),
                            new JProperty("Name", "ExMex"),
                            new JProperty("GuildStatus", 0),
                            new JProperty("Coin", 999999),
                            new JProperty("Exp", 0),
                            new JProperty("Level", 1),
                            new JProperty("Gem", 999999),
                            new JProperty("Vip", 1),
                            new JProperty("Avatar", "I11000301"),
                            new JProperty("RunePiece", 999999),
                            new JProperty("GuildCoin", 999999),
                            new JProperty("Voucher", 999999),
                            new JProperty("NextFirstWinTime", DateTime.Now.ToString("o"))
                        )
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestVersion")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestVersion"),
                    new JProperty("Result", "1"),
                    new JProperty("DataVersion", "1.0.0")
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestConfig")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestConfig"),
                    new JProperty("Result", "1"),
                    new JProperty("ConfigMap",
                        new JArray(
                            new JObject(
                                new JProperty("Name", "achievement_critical_rank"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "achievement_level_up_mail"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "guild_send_mail_max_count"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "limit_weekly_battle_coin"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "limit_weekly_battle_coin_bonus"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "limit_weekly_battle_coin_penalty"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "limit_weekly_battle_coin_bonus_credit"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "limit_weekly_battle_coin_penalty_credit"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "grow_up_client_battle_coin_reset_time"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "rune_page_price"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_voucher_treasure_once_price"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_voucher_treasure_five_times_price"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_gem_treasure_once_price"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_gem_treasure_five_times_price"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_coin_roll_rune_rare_level"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_coin_roll_rune_special_cycle"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_show_coin_roll_rune_desc"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_coin_roll_rune_once_price"),
                                new JProperty("Int", 1)
                            ),
                            new JObject(
                                new JProperty("Name", "shop_coin_roll_rune_five_times_price"),
                                new JProperty("Int", 1)
                            )
                        )
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestPlayerDeck")
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
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestFriendList")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestFriendList"),
                    new JProperty("Result", "1"),
                    new JProperty("FriendList",
                        new JArray(
                            /*new JObject(
                                new JProperty("PlayerId", 1)
                            )*/
                        )
                    ),
                    new JProperty("ForbidList",
                        new JArray()
                    ),
                    new JProperty("FriendCandidateList",
                        new JArray()
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestRecentGradeBattleLog")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestRecentGradeBattleLog"),
                    new JProperty("Result", "1"),
                    new JProperty("BattleLogs", new JArray())
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestCurrentSeasonInfo")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestCurrentSeasonInfo"),
                    new JProperty("Result", "1"),
                    new JProperty("Season", new JObject(
                            new JProperty("End", DateTime.Now.AddYears(2).ToString("o"))
                        )
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestMailList")
            {
                /*
Type": "RequestMailList",
"Category": 1.0,
"BeginIndex": 0.0,
"Length": 5.0
                 */
                var responseContent = new JObject(
                    new JProperty("Type", "RequestMailList"),
                    new JProperty("Result", "1"), // 3077
                    new JProperty("MailList", new JArray(
                        )
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestMyselfApplyingGuilds")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestMyselfApplyingGuilds"),
                    new JProperty("Result", "0") /*,
                    new JProperty("ApplyingGuilds", new JArray())
                    */
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestMyselfGuildDetail")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestMyselfGuildDetail"),
                    new JProperty("Result", "0") /*,
                    new JProperty("Guild", new JArray())
                    */
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestMyselfPreparingGuildDetail")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestMyselfPreparingGuildDetail"),
                    new JProperty("Result", "0") /*,
                    new JProperty("Guild", new JArray())
                    */
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestUnreadMailCount")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestUnreadMailCount"),
                    new JProperty("Result", "2") /*, // 1
                    new JProperty("UnreadFriendMailCount", "0"),
                    new JProperty("UnreadSystemMailCount", "0")*/
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestTemplate")
            {
                /*
JProperty.New("Category",0xffffffffffff))
                 */
                var responseContent = new JObject(
                    new JProperty("Type", "RequestTemplate"),
                    new JProperty("Result", "1"),
                    new JProperty("Roles", new JArray()),
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
                            new JProperty("StartEnd", new JArray(
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
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestValidLimitFreeRoleList")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestValidLimitFreeRoleList"),
                    new JProperty("Result", "0") /*, // 1
                    new JProperty("List", new JArray())*/
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestAddOnlineStatus")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestAddOnlineStatus")
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestServerRanks")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestServerRanks"),
                    new JProperty("Result", "0") /*, //1
                    new JProperty("Ranks", )
                    new JProperty("Self", )
                    */
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestPlayerBountyInfo")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestPlayerBountyInfo"),
                    new JProperty("Bounties", new JArray(
                            new JObject(
                                new JProperty("Id", 1),
                                new JProperty("Category", 1),
                                new JProperty("LevelLimit", 1)
                            )
                        )
                    )
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestReconnectStage")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestReconnectStage"),
                    new JProperty("Result", "0")
                    /*
                    new JProperty("Stage", "")
                    new JProperty("PartyInfo", "")
                    new JProperty("Content", "")
                    */
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestCurrentActivity")
            {
                var responseContent = new JObject(
                    new JProperty("Type", "RequestCurrentActivity"),
                    new JProperty("Result", "0")
                    /*
                    new JProperty("Activities", new JArray(
                    ActivityQuests
                            new JObject(
                                new JProperty("Id", 1)
                            )
                        )
                    )
                    */
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else if (response["Type"].ToString() == "RequestCreateParty")
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
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
            else
            {
                Console.WriteLine($"Unknown Packet ({channel}): {response}");
                var responseContent = new JObject(
                    new JProperty("Type", response["Type"])
                );
                SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
            }
#endif

            #endregion
        }

        private void Receive()
        {
            try
            {
                while (true)
                {
                    var headSize = Marshal.SizeOf(typeof(TGNetService.Header));
                    var head = new byte[headSize];
                    var headRead = 0;

                    while (headRead < headSize)
                    {
                        if (!_socket.Connected)
                        {
                            return;
                        }
                        _socket.ReceiveTimeout = TGNetService.ReceiveTimeout;
                        var n = _socket.Receive(head, headRead, headSize - headRead, SocketFlags.None);
                        headRead += n;
                    }

                    if (headRead != headSize)
                    {
                        throw new Exception("recv header failed " + headRead.ToString());
                    }

                    var header = Program.RawSerializer.RawDeserialize(head);
                    if (header.Sign != TGNetService.SIGN)
                    {
                        throw new Exception("recv header sign mismatch");
                    }

                    var channel = (TGNetService.NetServiceChannel) header.Channel;

                    var bytesExpected = (int) header.Bodysize;
                    var body = new byte[bytesExpected];
                    var bytesRead = 0;

                    while (bytesRead < bytesExpected)
                    {
                        _socket.ReceiveTimeout = 10000;
                        var n = _socket.Receive(body, bytesRead, bytesExpected - bytesRead, SocketFlags.None);
                        bytesRead += n;
                    }

                    var content = TGNetService.GZip(body, (int) header.ContentSize, out var length);

                    if (length != header.ContentSize)
                    {
                        throw new Exception("received body content size mismatch");
                    }

                    var str = Encoding.UTF8.GetString(content, 0, length);
                    if (JObject.Parse(str)["Type"].ToString() != "Beat")
                        Console.WriteLine(">>> ch:{3} receiving {0}->{1} body:{2}", header.ContentSize, header.Bodysize,
                            str, channel);

                    byte[] data = null;
                    if (header.DataSize > 0)
                    {
                        var dataExpected = (int) header.DataSize;
                        data = new byte[dataExpected];
                        var dataRead = 0;

                        while (dataRead < dataExpected)
                        {
                            _socket.ReceiveTimeout = 30000;
                            var n = _socket.Receive(data, dataRead, dataExpected - dataRead, SocketFlags.None);
                            dataRead += n;
                        }
                    }

                    HandlePkt(channel, JObject.Parse(str), data);

                    if (!_socket.Connected) return;
                }
            }
            catch (JsonException)
            {
                throw;
                // ignored
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // ignored
            }
        }

        public void SendBody(TGNetService.NetServiceChannel channel, string str, byte[] data)
        {
            //lock (soc)
            {
                var content = Encoding.UTF8.GetBytes(str);
                var body = TGNetService.GZip(content, 0, out var length);

                if (JObject.Parse(str)["Type"].ToString() != "Beat")
                    Console.WriteLine("<<< ch:{3} sending {0}->{1} body:{2}", content.Length, length, str, channel);

                var header = new TGNetService.Header
                {
                    Sign = TGNetService.SIGN,
                    Channel = (uint) channel,
                    Bodysize = (uint) length,
                    ContentSize = (uint) content.Length,
                    DataSize = (data == null) ? 0 : (uint) data.Length,
                };

                var head = Program.RawSerializer.RawSerialize(header);
                _socket.SendTimeout = 10000;
                if (_socket.Send(head) != Marshal.SizeOf(typeof(TGNetService.Header)))
                {
                    throw new Exception("send header failed");
                }

                _socket.SendTimeout = 30000;
                if (_socket.Send(body, length, SocketFlags.None) != length)
                {
                    throw new Exception("send body failed");
                }

                if (data != null)
                {
                    if (_socket.Send(data) != data.Length)
                    {
                        throw new Exception("send data failed");
                    }
                }
            }
        }
    }
}