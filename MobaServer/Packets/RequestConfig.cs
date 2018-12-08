using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestConfig
    {
        [Packet("RequestConfig")]
        public static void Handle(Packet packet)
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
                        ),
                        new JObject(
                            new JProperty("Name", "chat_public_cd_critical_level"),
                            new JProperty("Int", 1)
                        ),
                        new JObject(
                            new JProperty("Name", "chat_public_high_level_cd"),
                            new JProperty("Int", 1)
                        ),
                        new JObject(
                            new JProperty("Name", "chat_public_low_level_cd"),
                            new JProperty("Int", 1)
                        )
                    )
                )
            );
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}