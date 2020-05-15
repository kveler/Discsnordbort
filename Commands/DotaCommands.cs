using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class DotaCommands : BaseCommandModule
    {
        [Command("dota")]
        [Description("Dota commands")]
        public async Task Dota(CommandContext ctx, string para1 = "", string steamUsername = "")
        {
            await ctx.Channel.SendMessageAsync("Jaja dit komt ooit nog wel").ConfigureAwait(false);
        }
    }
}
