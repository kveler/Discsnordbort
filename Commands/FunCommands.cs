using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("Hoe")]
        [Description("Alles goed?")]
        public async Task Hoe(CommandContext ctx, string woord1, string woord2)
        {
            string sentence = (woord1 + " " + woord2);

            switch(sentence)
            {
                case "gaat het?":
                    await ctx.Channel.SendMessageAsync("Ik heet helemaal geen Hennie godverdomme kutmongool").ConfigureAwait(false);
                    break;

                case "heet je":
                    await ctx.Channel.SendMessageAsync(":regional_indicator_j: :regional_indicator_a: :regional_indicator_n:").ConfigureAwait(false);
                    break;

                default:
                    await ctx.Channel.SendMessageAsync("Dit snap ik nog niet :(").ConfigureAwait(false);
                    break;
            }

        }

        [Command("krijg")]
        [Description("Krijg het zelf maar lekker")]
        public async Task Hoe(CommandContext ctx, string woord1)
        {
                await ctx.Channel.SendMessageAsync("Krijg zelf " + woord1).ConfigureAwait(false);
        }
    }
}
