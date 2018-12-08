using System;
using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class Auth
    {
        private class InAuthPacket
        {
            public readonly string AccountId;
            public readonly string SessionKey;

            public InAuthPacket(Packet packet)
            {
                var request = packet.Content;

                if (request.ContainsKey("AccountId"))
                    AccountId = request["AccountId"].ToString();

                if (request.ContainsKey("SessionKey"))
                    SessionKey = request["SessionKey"].ToString();
            }
        }

        private class OutAuthPacket : IOutPacket
        {
            /// <summary>
            /// Can be either 1, 0x0304 or 0x0303
            /// 1 = Login Success
            /// 0x0304 = Invalid Session (does nothing on client side)
            /// 0x0303 = Session Expired (Executes ReLoginToGetNewData and reconnects to the login server)
            /// </summary>
            public int Result;

            public override JObject GetContent()
            {
                var responseContent = new JObject(
                    new JProperty("Type", "Auth"),
                    new JProperty("Result", Result)
                );

                return responseContent;
            }
        }

        [Packet("Auth")]
        public static void Handle(Packet packet)
        {
            var auth = new InAuthPacket(packet);
            var result = 1;
            if (auth.SessionKey != "123456")
            {
                Console.WriteLine(auth.SessionKey);
                result = 0x0303;
            }

            var authAck = new OutAuthPacket
            {
                Result = result
            };

            // AccountId
            // SessionKey
            /*var responseContent = new JObject(
                new JProperty("Type", "Auth"),
                new JProperty("Result", "1") // 0x0303 = ReLoginToGetNewData
            );
           packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
           */
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, authAck.GetContent().ToString(),
                authAck.GetData());
        }
    }
}