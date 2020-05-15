using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot
{
    class LolTftReply
    {
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("summonerLevel")]
        public string SummonerLevel { get; private set; }
    }
}
