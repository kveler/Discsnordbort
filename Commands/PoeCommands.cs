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
    public class PoeCommands : BaseCommandModule
    {
        [Command("poe")]
        [Description("Path Of Exile commands")]
        public async Task PathOfExile(CommandContext ctx, string para1, string username, string character = "")
        {
            Console.WriteLine("Hennie poe " + para1 + " " + username + " " + character);
            switch (para1)
            {
                case "account":
                    var accountClient = new RestClient("https://www.pathofexile.com/character-window/get-characters?accountName=" + username);
                    accountClient.Timeout = -1;
                    var accountRequest = new RestRequest(Method.POST);
                    IRestResponse accountResponse = accountClient.Execute(accountRequest);

                    if(accountResponse.Content == "{\"error\":{\"code\":6,\"message\":\"Forbidden\"}}")
                    {
                        await ctx.Channel.SendMessageAsync("Je account staat op private :crying_cat_face:").ConfigureAwait(false);
                        break;
                    }

                    dynamic jsonAccountResult = JsonConvert.DeserializeObject(accountResponse.Content);

                    string accountMessage = "Characters for account: " + username + "\n";

                    foreach(var accountItem in jsonAccountResult)
                    {
                            accountMessage += "Name: " + accountItem.name + " Level: " + accountItem.level + "\n";
                    }

                    await ctx.Channel.SendMessageAsync(accountMessage).ConfigureAwait(false);
                    break;

                case "character":
                    string endpoint = "https://www.pathofexile.com/character-window/get-items?accountName=" + username + "&character=" + character;
                    var characterClient = new RestClient("https://www.pathofexile.com/character-window/get-items?accountName=" + username + "&character=" + character);
                    characterClient.Timeout = -1;
                    var characterRequest = new RestRequest(Method.POST);
                    IRestResponse characterResponse = characterClient.Execute(characterRequest);
                    dynamic jsonCharacterResult = JsonConvert.DeserializeObject(characterResponse.Content);
                    string itemMessage = "Items for character: " + character + "\n";

                    foreach(var characterItem in jsonCharacterResult.items)
                    {
                        itemMessage += "Item name: " + characterItem.name + ", Item level: " + characterItem.ilvl + "\n";
                    }

                    await ctx.Channel.SendMessageAsync(itemMessage).ConfigureAwait(false);
                    break;

                default:
                    await ctx.Channel.SendMessageAsync("Dit snap ik nog niet :(").ConfigureAwait(false);
                    break;
            }
        }
    }
}
