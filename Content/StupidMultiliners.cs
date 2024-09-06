using InformativeTooltips.Content.GlobalTooltips;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
namespace InformativeTooltips.Content.StupidMultiliners
{
    public class SquireGreavesTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string SquireGreavesTooltip1 { get; private set; }
        public static string SquireGreavesTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SquireGreaves;
        }
        public override void SetStaticDefaults()
        {
            SquireGreavesTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), 15, "summon", "damage");
            SquireGreavesTooltip2 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), 15, "melee critical strike chance", "and movement speed");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(SquireGreavesTooltip1))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", SquireGreavesTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", SquireGreavesTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class ValhallaHelmetTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string ValhallaHelmetTooltip1 { get; private set; }
        public static string ValhallaHelmetTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SquireAltHead;
        }
        public override void SetStaticDefaults()
        {
            ValhallaHelmetTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipNum"), "max number of sentries", "2");
            ValhallaHelmetTooltip2 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "10", "summon", "and melee damage");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(ValhallaHelmetTooltip1) || tooltipLine.Text.Contains(ValhallaHelmetTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", ValhallaHelmetTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", ValhallaHelmetTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class ShinobiHelmetTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string ShinobiHelmetTooltip1 { get; private set; }
        public static string ShinobiHelmetTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.MonkAltHead;
        }
        public override void SetStaticDefaults()
        {
            ShinobiHelmetTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipNum"), "max number of sentries", "2");
            ShinobiHelmetTooltip2 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "20", "summon", "and melee damage");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(ShinobiHelmetTooltip1) || tooltipLine.Text.Contains(ShinobiHelmetTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", ShinobiHelmetTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", ShinobiHelmetTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class ShroomiteBreastplateTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string ShroomiteBreastplateTooltipLine { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.ShroomiteBreastplate;
        }
        public override void SetStaticDefaults()
        {
            ShroomiteBreastplateTooltipLine = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "13", "ranged damage", "and critical strike chance");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains('&'))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", ShroomiteBreastplateTooltipLine);
            tooltips.Add(Tooltip1);
        }
    }
    public class DarkArtistHatTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string DarkArtistHatTooltipLine { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.ApprenticeAltHead;
        }
        public override void SetStaticDefaults()
        {
            DarkArtistHatTooltipLine = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "15", "summon", "and magic damage");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains('&'))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", DarkArtistHatTooltipLine);
            tooltips.Add(Tooltip1);
        }
    }
    public class ApprenticeHatTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string ApprenticeHatTooltip1 { get; private set; }
        public static string ApprenticeHatTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.ApprenticeHat;
        }
        public override void SetStaticDefaults()
        {
            ApprenticeHatTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipNum"), "max number of sentries", "1");
            ApprenticeHatTooltip2 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "10", "magic damage", "and 10% reduced mana cost");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(ApprenticeHatTooltip1) || tooltipLine.Text.Contains(ApprenticeHatTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", ApprenticeHatTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", ApprenticeHatTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class ApprenticeTrousersTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string ApprenticeTrousersTooltip1 { get; private set; }
        public static string ApprenticeTrousersTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.ApprenticeTrousers;
        }
        public override void SetStaticDefaults()
        {
            ApprenticeTrousersTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "10", "summon", "damage");
            ApprenticeTrousersTooltip2 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "20", "magic critical strike chance", "and movement speed");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(ApprenticeTrousersTooltip1) || tooltipLine.Text.Contains(ApprenticeTrousersTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", ApprenticeTrousersTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", ApprenticeTrousersTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class MonkPantsTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string MonkPantsTooltip1 { get; private set; }
        public static string MonkPantsTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.MonkPants;
        }
        public override void SetStaticDefaults()
        {
            MonkPantsTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "10", "summon", "damage");
            MonkPantsTooltip2 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "10", "melee critical strike chance", "and 20% movement speed");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(MonkPantsTooltip1) || tooltipLine.Text.Contains(MonkPantsTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", MonkPantsTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", MonkPantsTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class RedRidingDressTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string RedRidingDressTooltip1 { get; private set; }
        public static string RedRidingDressTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.HuntressAltShirt;
        }
        public override void SetStaticDefaults()
        {
            RedRidingDressTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "25", "summon", "and ranged damage");
            RedRidingDressTooltip2 = Language.GetTextValue("Mods.InformativeTooltips.Items.RedRidingDress.Tooltip2");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(RedRidingDressTooltip1) || tooltipLine.Text.Contains(RedRidingDressTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", RedRidingDressTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", RedRidingDressTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
    public class HuntressSuitTooltip : ManualTooltipFixes
    {
        public override bool InstancePerEntity => true;
        public static string HuntressSuitTooltip1 { get; private set; }
        public static string HuntressSuitTooltip2 { get; private set; }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.HuntressJerkin;
        }
        public override void SetStaticDefaults()
        {
            HuntressSuitTooltip1 = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), "20", "summon", "and ranged damage");
            HuntressSuitTooltip2 = Language.GetTextValue("Mods.InformativeTooltips.Items.HuntressSuit.Tooltip2");
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Text.Contains(HuntressSuitTooltip1) || tooltipLine.Text.Contains(HuntressSuitTooltip2))
                {
                    tooltips.RemoveAt(i);
                }
            }
            var Tooltip1 = new TooltipLine(Mod, "Line1", HuntressSuitTooltip1);
            var Tooltip2 = new TooltipLine(Mod, "Line2", HuntressSuitTooltip2);
            tooltips.Add(Tooltip1);
            tooltips.Add(Tooltip2);
        }
    }
}