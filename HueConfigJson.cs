using Newtonsoft.Json;

namespace DiscordBot
{
    class HueConfigJson
    {
        [JsonProperty("ip")]
        public string Ip { get; private set; }
        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}