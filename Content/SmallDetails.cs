using InformativeTooltips.Common.Configs;
using InformativeTooltips.Content.GlobalTooltips;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace InformativeTooltips.Content.SmallDetails
{
    public class AntiReadingMeasures : GlobalTooltipsBase
    {
        public AntiReadingMeasures() : base(4) { }
    }
    public class DefaultTooltipColor : AntiReadingMeasures
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            return;
            foreach (var line in tooltips)
            {
                if (line.OverrideColor == null)
                {
                    line.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().DefaultColor;
                }
                else if (line.Text.Contains("[c"))
                {
                    string[] parts = line.Text[(line.Text.IndexOf(']') + 1)..].Trim().Split(' ');
                    if (parts.Length > 0)
                    {
                        string color = ColorToHex(ModContent.GetInstance<ArmorDetailedConfig>().DefaultColor);
                        parts[^1] = $"[c/{color}:{parts[^1]}]";
                        line.Text = "";
                        line.Text += parts;
                    }
                }
            }
        }
        static string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
    public class ItemValueAdditions : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return !entity.social;
        }
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
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
                var cum = ModContent.GetInstance<ArmorDetailedConfig>().CoinTooltipToggle ? "Coins" : "Vanilla";
                price = plat != 0 ? price + string.Format(Language.GetTextValue($"Mods.InformativeTooltips.Items.Price.{cum}.plat"), plat) : price;
                price = gold != 0 ? price + string.Format(Language.GetTextValue($"Mods.InformativeTooltips.Items.Price.{cum}.gold"), gold) : price;
                price = silv != 0 ? price + string.Format(Language.GetTextValue($"Mods.InformativeTooltips.Items.Price.{cum}.silv"), silv) : price;
                price = copp != 0 ? price + string.Format(Language.GetTextValue($"Mods.InformativeTooltips.Items.Price.{cum}.copp"), copp) : price;
                if (plat == 0 && gold == 0 && silv == 0 && copp == 0)
                {
                    price += " 0";
                }
                int index = tooltips.FindIndex(line => line.Name == "Price");
                var Price = new TooltipLine(Mod, "DefaultPrice", price) { OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor };
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
                        if (ModContent.GetInstance<ArmorDetailedConfig>().CoinTooltipToggle)
                        {
                            tooltips[index].Text = ShopValueTooltipUpgrade(tooltips[index].Text);
                        }

                        if (ModContent.GetInstance<ArmorDetailedConfig>().ShopValueComparisonToggle) 
                        { 
                            tooltips.Insert(index, multLine); 
                        }

                        if (ModContent.GetInstance<ArmorDetailedConfig>().DefaultValueToggle)
                        {
                            tooltips.Insert(index, Price);
                        }
                    }
                }
                else
                {
                    string[] search =
                    [
                        "Prefix",
                        "Expert",
                        "Master",
                        "Journey",
                        "Bestiary",
                        "Etherian",
                        "OneDropLogo",
                    ];
                    int i = tooltips.Count;
                    foreach (var text in search)
                    {
                        int y = tooltips.FindIndex(line => line.Name.Contains(text));
                        if (y != -1 && y <= i)
                        {
                            i = y;
                        }
                    }
                    if (ModContent.GetInstance<ArmorDetailedConfig>().DefaultValueToggle)
                    {
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
        }
        public static string ShopValueTooltipUpgrade(string text)
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
            string[] parts = text[(text.IndexOf(':') + 1)..].Trim().Split(' ');
            // Currency type mapping
            var currencyTypeMap = new Dictionary<string, int>
            {
                { "platinum", 74 },
                { "gold", 73 },
                { "silver", 72 },
                { "copper", 71 }
            };

            // StringBuilder for the output
            var result = string.Empty;

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
                        result += $"[i/s{value}:{type}]";
                    }
                }

            }

            // Return the result as a string, prefixed with "value: "
            return $"{start} {result}";
        }
        public static int ShopValueTooltipExtraction(string text)
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
            string[] parts = text[(text.IndexOf(':') + 1)..].Trim().Split(' ');

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