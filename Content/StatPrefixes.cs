using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AccessoryPrefixesPlus.Content.StatPrefixes
{
    public class Vital : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 1.1f; // same chance as arcane
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3225f; // same value as arcane
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.statLifeMax2 += 20; // because I'm not stupid
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccMaxHealth", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Vital.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }

    public class Sneaky : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3225f; // same value as arcane
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.aggro -= 50; // because I'm not stupid
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccAggro", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Sneaky.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }

    public class Brave : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3225f; // same value as arcane
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.aggro -= 50; // because I'm not stupid
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccAggro", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Brave.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }

    public class Masterful : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 1.1f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3f;
        }
        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<PrefixPlayer.AccessoryPrefixPlayer>().size += 0.4f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccSize", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Masterful.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }

    public class Peaceful : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 1.2f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.12f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<PrefixPlayer.AccessoryPrefixPlayer>().spawnrate += 0.05f;
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccPeace", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Peaceful.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }

    public class Agile : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            return 0.9f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.4f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.jumpSpeedBoost += 0.25f; // because I'm not stupid
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tooltip = new TooltipLine(Mod, "PrefixAccJump", Language.GetTextValue("Mods.AccessoryPrefixesPlus.Prefixes.Agile.Tooltip"));
            tooltip.IsModifier = true; // because it better be compatible
            yield return tooltip;
        }
    }
}