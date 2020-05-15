using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DiscordBot.Commands
{
    public class HueCommands : BaseCommandModule
    {
        [Command("hue")]
        [Description("My set of hue commands")]
        public async Task Hue(CommandContext ctx, string para1, string para2 = "", string para3 = "")
        {
            var jsonString = string.Empty;

            using (var fs = File.OpenRead("HueConfig.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                jsonString = await sr.ReadToEndAsync().ConfigureAwait(false);

            var hueConfigJson = JsonConvert.DeserializeObject<HueConfigJson>(jsonString);

            string ip = hueConfigJson.Ip;
            string username = hueConfigJson.Token;
            string baseUrl = "http://" + ip + "/api/" + username;

            switch (para1)
            {
                case "status":
                    var client = new RestClient(baseUrl + "/lights");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    dynamic jsonResult = JsonConvert.DeserializeObject(response.Content);
                    string message = "Aangesloten lampen \n \n";
                    foreach (var lampItem in jsonResult)
                    {
                        string lampId = ((JToken)lampItem).Path;
                        foreach (var realLampItem in lampItem)
                        {
                            string status = "Geen idee";
                            bool lampStatus = realLampItem.state.on;

                            if (lampStatus)
                            {
                                status = ":white_check_mark:";
                            }
                            else
                            {
                                status = ":negative_squared_cross_mark:";
                            }

                            message += "Lamp naam: " + realLampItem.name + ", ID: " + lampId + ", status: " + status + "\n";
                        }
                    }
                    await ctx.Channel.SendMessageAsync(message).ConfigureAwait(false);
                    break;
                case "lamp":
                    string lamp = para2;
                    string setLampStatus = "false";

                    if (para3 == "aan")
                    {
                        setLampStatus = "true";
                    }

                    string json = "{\"on\":" + setLampStatus + "}";

                    var lampClient = new RestClient(baseUrl + "/lights/" + lamp + "/state");
                    lampClient.Timeout = -1;
                    var lampRequest = new RestRequest(Method.PUT);
                    lampRequest.AddParameter("application/json", json, ParameterType.RequestBody);
                    IRestResponse lampResponse = lampClient.Execute(lampRequest);

                    await ctx.Channel.SendMessageAsync("Lamp " + para2 + " staat nu " + para3).ConfigureAwait(false);
                    break;
                case "lampen":
                    string setLampenStatus = "true";

                    if(para2 == "zwart")
                    {
                        setLampenStatus = "false";
                    }

                    if (para2 == "uit")
                    {
                        setLampenStatus = "false";
                    }

                    var lampenClient = new RestClient(baseUrl + "/lights");
                    lampenClient.Timeout = -1;
                    var lampenRequest = new RestRequest(Method.GET);
                    IRestResponse lampenResponse = lampenClient.Execute(lampenRequest);
                    dynamic lampenJsonResult = JsonConvert.DeserializeObject(lampenResponse.Content);
                    foreach (var lampItem in lampenJsonResult)
                    {
                        string lampenJson = "{\"on\":" + setLampenStatus + "}";

                        if (GetHueStringToHueInt(para2) != 0)
                        {
                            lampenJson = "{\"on\":" + setLampenStatus + ",\"hue\":" + GetHueStringToHueInt(para2) + ",\"sat\":" + GetHueStringToSatInt(para2) + "}";
                        }
                        else
                        if (para2 == "disco")
                        {
                            lampenJson = "{\"on\":" + setLampenStatus + ",\"effect\":\"colorloop\"}";
                        }

                        var lampenLoopClient = new RestClient(baseUrl + "/lights/" + ((JToken)lampItem).Path + "/state");
                        lampenLoopClient.Timeout = -1;
                        var lampenLoopRequest = new RestRequest(Method.PUT);
                        lampenLoopRequest.AddParameter("application/json", lampenJson, ParameterType.RequestBody);
                        IRestResponse lampenLoopResponse = lampenLoopClient.Execute(lampenLoopRequest);
                    }

                    await ctx.Channel.SendMessageAsync("Alle lampen staan nu " + para2).ConfigureAwait(false);
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("Jaja komt eraan").ConfigureAwait(false);
                    break;
            }

        }

        static int GetHueStringToHueInt(string color)
        {
            switch (color)
            {
                case "blauw":
                    return 43690;
                case "rood":
                    return 65535;
                case "groen":
                    return 25500;
                case "paars":
                    return 49172;
                case "roze":
                    return 54612;
                case "oceaan":
                    return 39616;
                case "geel":
                    return 10922;
                case "oranje":
                    return 6375;
                case "wit":
                    return 1;
                default:
                    return 0;
            }
        }

        static int GetHueStringToSatInt(string color)
        {
            switch (color)
            {
                case "blauw":
                    return 254;
                case "rood":
                    return 254;
                case "groen":
                    return 254;
                case "paars":
                    return 254;
                case "roze":
                    return 208;
                case "oceaan":
                    return 184;
                case "geel":
                    return 254;
                case "oranje":
                    return 6375;
                case "wit":
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
