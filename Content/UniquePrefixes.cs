using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AccessoryPrefixesPlus.Content.UniquePrefixes
{
    public class Patient : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.2f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.breathMax += 600;
            player.breathCD += 600;
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
            ModContent.GetInstance<BuffDetour>().HealthyTime += 0.03f;
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
            ModContent.GetInstance<BuffDetour>().ResilientTime -= 0.03f;
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
            return (int)mult * buffTime;
        }
    }
}