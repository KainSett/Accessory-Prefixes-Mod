using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace AccessoryPrefixesPlus.Content.VanillaPrefixes
{
    public class Warding : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.accessory && item.prefix == PrefixID.Warding && !item.social;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.endurance += 0.03f;
            player.statDefense -= 4; // Remove the default defense provided by Warding
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Find and remove the default defense tooltip
            int index = tooltips.FindIndex(t => t.Name == "PrefixAccDefense" && t.Mod == "Terraria");
            if (index != -1)
            {
                tooltips[index].Text = Language.GetTextValue("Mods.SentinelMod.Prefixes.Warding.Tooltip");
            }
        }
    }
    public class Quick : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.accessory && item.prefix == PrefixID.Warding && !item.social;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.runAcceleration += 0.03f;
            player.moveSpeed -= 0.01f;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int index = tooltips.FindIndex(t => t.Name == "PrefixAccMoveSpeed" && t.Mod == "Terraria");
            if (index != -1)
            {
                tooltips[index].Text = Language.GetTextValue("Mods.SentinelMod.Prefixes.Quick.Tooltip");
            }
        }
    }
}