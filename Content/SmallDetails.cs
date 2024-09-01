using InformativeTooltips.Common.Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using static System.Net.Mime.MediaTypeNames;

namespace InformativeTooltips.Content.SmallDetails
{
    public class LilAdditions : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return !entity.social;
        }
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ModContent.GetInstance<ArmorDetailedConfig>().SmallDetailsToggle) { return; }
            if (!Main.keyState.IsKeyDown(Keys.LeftShift) && !Main.keyState.IsKeyDown(Keys.RightShift))
            {
                int value = item.value / 5;
                int plat = 0;
                int gold = 0;
                int silv = 0;
                int copp = 0;
                while (value >= 1000000) { ++plat; value -= 1000000; }
                while (value >= 10000 && value < 1000000) { ++gold; value -= 10000; }
                while (value >= 100 && value < 10000) { ++silv; value -= 100; }
                while (value >= 1 && value < 100) { ++copp; value -= 1; }
                string price = Language.GetTextValue("Mods.InformativeTooltips.Items.Price.Value");
                price = plat != 0 ? price + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.Price.plat"), plat) : price;
                price = gold != 0 ? price + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.Price.gold"), gold) : price;
                price = silv != 0 ? price + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.Price.silv"), silv) : price;
                price = copp != 0 ? price + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.Price.copp"), copp) : price;
                if (plat == 0 && gold == 0 && silv == 0 && copp == 0)
                {
                    price += " 0";
                }
                int index = tooltips.FindIndex(line => line.Name == "Price");
                var Price = new TooltipLine(Mod, "DefaultPrice", price);
                Price.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                if (index != -1)
                {
                    if (item.value != 0)
                    {
                        var line = tooltips[tooltips.FindIndex(line => line.Name == "Price")].Text;
                        var defaultprice = Item.buyPrice(plat, gold, silv, copp);
                        int shopprice = ShopValueTooltipExtraction(line);
                        float multiplier = (float)shopprice / defaultprice;
                        string formattedResult = multiplier % 1 == 0 ? $"{multiplier:0}" : $"{multiplier:0.##}";
                        var multLine = new TooltipLine(Mod, "PriceMultiplier", Language.GetTextValue("Mods.InformativeTooltips.Items.Price.shop") + $" {formattedResult}");
                        multLine.OverrideColor = multiplier < 1 ? ModContent.GetInstance<ArmorDetailedConfig>().NegativeColor : multLine.OverrideColor;
                        multLine.OverrideColor = multiplier > 1 ? ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor : multLine.OverrideColor;
                        var coins = tooltips[index].Text;
                        tooltips[index].Text = ShopValueTooltipUpgrade(tooltips[index].Text);
                        tooltips.Insert(index, multLine);
                        tooltips.Insert(index, Price);
                    }
                }
                else
                {
                    string[] search = new string[7];
                    search[0] = "Prefix";
                    search[1] = "Expert";
                    search[2] = "Master";
                    search[3] = "Journey";
                    search[4] = "Bestiary";
                    search[5] = "Etherian";
                    search[6] = "OneDropLogo";
                    int i = tooltips.Count;
                    foreach (var text in search)
                    {
                        int y = tooltips.FindIndex(line => line.Name.Contains(text));
                        if (y != -1 && y <= i)
                        {
                            i = y;
                        }
                    }
                    if (i == tooltips.Count)
                    {
                        tooltips.Add(Price);
                    }
                    else 
                    { 
                        tooltips.Insert(i, Price); 
                    }
                }
            }
        }
        public string ShopValueTooltipUpgrade(string text)
        {
            // Split the string by commas
            int count = text.Length - 1;
            for (int x = 0; x < count; x++)
            {
                char remove = text[x];
                char postremove = text[x + 1];
                if (remove == ' ' && char.IsLetter(postremove))
                {
                    text = $"{text.Remove(x)}{text[(x + 1)..]}";
                    --count;
                }
            }
            // Trim and process the input string
            var start = text.Remove(text.IndexOf(':') + 1);
            string[] parts = text.Substring(text.IndexOf(':') + 1).Trim().Split(' ');
            // Currency type mapping
            var currencyTypeMap = new Dictionary<string, int>
            {
                { "platinum", 74 },
                { "gold", 73 },
                { "silver", 72 },
                { "copper", 71 }
            };

            // StringBuilder for the output
            var result = new System.Text.StringBuilder();

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                var notext = "";
                foreach (var l in part)
                {
                    if (char.IsDigit(l))
                    {
                        notext += l;
                    }
                }
                var value = int.Parse(notext);

                foreach (var coin in currencyTypeMap)
                {
                    if (part.Contains($"{coin.Key}"))
                    {
                        var type = coin.Value;
                        result.Append($"[i/s{value}:{type}]");
                    }
                }

            }

            // Return the result as a string, prefixed with "value: "
            return $"{start} {result.ToString().Trim()}";
        }
        public int ShopValueTooltipExtraction(string text)
        {
            int platinum = 0, gold = 0, silver = 0, copper = 0;

            // Split the string by commas
            int count = text.Length - 1;
            for (int x = 0; x < count; x++)
            {
                char remove = text[x];
                char postremove = text[x + 1];
                if (remove == ' ' && char.IsLetter(postremove))
                {
                    text = $"{text.Remove(x)}{text[(x + 1)..]}";
                    --count;
                }
            }
            string[] parts = text.Substring(text.IndexOf(':') + 1).Trim().Split(' ');

            foreach (string part in parts)
            {
                if (part.Contains("platinum"))
                    platinum = ExtractValue(part);
                else if (part.Contains("gold"))
                    gold = ExtractValue(part);
                else if (part.Contains("silver"))
                    silver = ExtractValue(part);
                else if (part.Contains("copper"))
                    copper = ExtractValue(part);
            }

            // Convert all to copper
            return (platinum * 100 * 100 * 100) + (gold * 100 * 100) + (silver * 100) + copper;
        }
        public static int ExtractValue(string part)
        {
            // Trim any leading/trailing whitespace
            part = part.Trim();

            // Initialize an empty string to hold the number
            string numberStr = "";

            // Iterate through each character in the string
            foreach (char c in part)
            {
                // If the character is a digit, add it to numberStr
                if (char.IsDigit(c))
                {
                    numberStr += c;
                }
                else
                {
                    // As soon as you encounter a non-digit, break the loop
                    break;
                }
            }

            // Parse the number string to an integer and return
            return int.Parse(numberStr);
        }
    }
}