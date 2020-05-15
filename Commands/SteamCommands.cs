using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class SteamCommands : BaseCommandModule
    {
        [Command("steam")]
        [Description("Steam commands")]
        public async Task Steam(CommandContext ctx, string para1 = "", string steamUsername = "")
        {
            await ctx.Channel.SendMessageAsync("Jaja dit komt ooit nog wel").ConfigureAwait(false);
        }
    }
    //Years of service
    //Amount of games
    //Total playtime
}
