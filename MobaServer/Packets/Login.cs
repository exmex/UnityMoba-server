using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class Login
    {
        private class InLoginPacket
        {
            public readonly string Username;
            public readonly string Password;

            /// <summary>
            /// All
            /// </summary>
            public readonly string DeviceType;

            /// <summary>
            /// 6969696969696969696
            /// </summary>
            public readonly string DeviceId;

            public InLoginPacket(Packet packet)
            {
                var request = packet.Content;

                if (request.ContainsKey("UserName"))
                    Username = request["UserName"].ToString();

                if (request.ContainsKey("Password"))
                    Password = request["Password"].ToString();

                if (request.ContainsKey("DeviceType"))
                    DeviceType = request["DeviceType"].ToString();

                if (request.ContainsKey("DeviceId"))
                    DeviceId = request["DeviceId"].ToString();
            }
        }

        private class OutLoginPacket : IOutPacket
        {
            /// <summary>
            /// The result of the login
            /// 1 = Success
            /// 0 = Error
            /// </summary>
            public string Result;

            public string AccountId;

            public string SessionKey;

            public override JObject GetContent()
            {
                var responseContent = new JObject(
                    new JProperty("Type", "Login"),
                    new JProperty("Result", Result),
                    new JProperty("AccountId", AccountId),
                    new JProperty("Session", SessionKey)
                );
                return responseContent;
            }
        }

        [Packet("Login")]
        public static void Handle(Packet packet)
        {
            var login = new InLoginPacket(packet);
            var result = 0;
            
            if (login.Username == "hello" && login.Password == "world")
            {
                result = 1;
            }
            var loginAck = new OutLoginPacket
            {
                Result = result.ToString(), AccountId = "1", SessionKey = "123456"
            };
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, loginAck.ToString(), loginAck.GetData());
        }
    }
}