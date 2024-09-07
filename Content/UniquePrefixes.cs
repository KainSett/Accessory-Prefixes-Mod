using System.Text.RegularExpressions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace AccessoryPrefixesPlus.Content.UniquePrefixes
{
    public class Royal : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<PrefixPlayer.AccessoryPrefixPlayer>().power = player.GetModPlayer<PrefixPlayer.AccessoryPrefixPlayer>().power == 0 ? 0.1f : player.GetModPlayer<PrefixPlayer.AccessoryPrefixPlayer>().power * 1.5f;
        }
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.2f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccBreath", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Royal.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }
    public class Patient : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.2f;
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<PrefixPlayer.AccessoryPrefixPlayer>().breathTime += 20;
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccBreath", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Patient.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }
    public class Healthy : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<BuffDetour>().HealthyTime += 0.05f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccBuff", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Healthy.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }
    public class Resilient : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3f;
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<BuffDetour>().HealthyTime -= 0.05f;
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccBuff", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Resilient.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }
    public class BuffDetour : ModPlayer
    {
        public float HealthyTime = 1f;
        public float ResilientTime = 1f;
        public override void Load()
        {
            Terraria.On_Player.AddBuff_DetermineBuffTimeToAdd += HealthyBuffTime;
        }

        private static int HealthyBuffTime(On_Player.orig_AddBuff_DetermineBuffTimeToAdd orig, Player self, int type, int time1)
        {
            int buffTime = orig(self, type, time1);
            float mult = 1f;

            if (BuffID.Sets.IsAFlaskBuff[type] || Main.buffNoTimeDisplay[type] || time1 == 2)
            {
                return buffTime;
            }
            else if (!Main.debuff[type])
            {
                mult = self.GetModPlayer<BuffDetour>().HealthyTime;
            }
            else
            {
                mult = self.GetModPlayer<BuffDetour>().ResilientTime;
            }
            return (int)(mult * buffTime);
        }
        public override void ResetEffects()
        {
            HealthyTime = 1f;
            ResilientTime = 1f;
        }
    }
    public class BuffTimeItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int index = tooltips.FindIndex(line => line.Name == "BuffTime");
            if (index >= 0)
            {
                var line = tooltips[index].Text;
                Regex number = new Regex(@"\d+");
                var num = number.Match(line).Value;
                if (int.TryParse(num, out int Num))
                {
                    int ind = line.IndexOf(num);
                    Num = (int)(60 * Num * Main.LocalPlayer.GetModPlayer<BuffDetour>().HealthyTime);
                    var sec = Num % 60;
                    Num -= sec;
                    Num /= 60;
                    ind += 2;
                    tooltips[index].Text = $"{line.Remove(ind - 2)}{Num}{line[ind..]}";
                    var toInsert = tooltips[index].Text.LastIndexOf(' ');
                    tooltips[index].Text = sec != 0 ? tooltips[index].Text.Insert(toInsert, $" {sec} {Language.GetTextValue("Mods.AccessoryPrefixesPlus.Other.seconds")}") : tooltips[index].Text;
                }
            }
        }
    }
}