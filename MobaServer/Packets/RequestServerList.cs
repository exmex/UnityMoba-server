using Newtonsoft.Json.Linq;

namespace MobaServer.Packets
{
    public class RequestServerList
    {
        [Packet("RequestServerList")]
        public static void Handle(Packet packet)
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
            packet.Client.SendBody(TGNetService.NetServiceChannel.Primary, responseContent.ToString(), new byte[0]);
        }
    }
}