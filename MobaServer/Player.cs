using Newtonsoft.Json.Linq;

namespace MobaServer
{
    public class Player : ISerializable
    {
        public int Id;
        public string Name;
        public string Avatar;
        public string FrameId;
        public int Vip;
        public int Level;
        public string GradeName;
        public string GuildName;
        public int GuildPosition;

        /// <summary>
        /// 0 = Offline
        /// 1 = Online
        /// </summary>
        public int Status;

        public JObject Serialize()
        {
            return new JObject(
                new JProperty(
                    "PlayerId", Id
                ),
                new JProperty(
                    "PlayerName", Name
                ),
                new JProperty(
                    "PlayerAvatar", Avatar
                ),
                new JProperty(
                    "AvatarFrameId", FrameId
                ),
                new JProperty(
                    "Vip", Vip
                ),
                new JProperty(
                    "Level", Level
                ),
                new JProperty(
                    "GradeName", GradeName
                ),
                new JProperty(
                    "GuildName", GuildName
                ),
                new JProperty(
                    "GuildPosition", GuildPosition
                ),
                new JProperty(
                    "Status", Status
                )
            );
        }

        public void Deserialize(JObject content)
        {
            throw new System.NotImplementedException();
        }
    }
}