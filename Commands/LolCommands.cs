using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class LolCommands : BaseCommandModule
    {
        [Command("lol")]
        [Description("Doe league commands")]
        public async Task LeagueOfLegends(CommandContext ctx, string para1, string username)
        {
            switch (para1)
            {
                case "tftlevel":
                    var client = new RestClient("https://euw1.api.riotgames.com/tft/summoner/v1/summoners/by-name/" + username);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("X-Riot-Token", "RGAPI-8c75386f-79df-4804-b44d-9c7e5c2aa559"); //Verloopt dagelijks
                    IRestResponse response = client.Execute(request);

                    var result = JsonConvert.DeserializeObject<LolTftReply>(response.Content);

                    string message = "Name: " + result.Name + "\nLevel: " + result.SummonerLevel;

                    await ctx.Channel.SendMessageAsync(message).ConfigureAwait(false);
                    break;

                default:
                    await ctx.Channel.SendMessageAsync("Dit snap ik nog niet :(").ConfigureAwait(false);
                    break;
            }
        }
    }
}
