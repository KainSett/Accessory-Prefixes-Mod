using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using System;
using Terraria.GameInput;
using System.Linq;
using InformativeTooltips.Common.Configs;
namespace InformativeTooltips.Content.BetterTooltips
{
    public class Food : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && (entity.buffType == BuffID.WellFed || entity.buffType == BuffID.WellFed2 || entity.buffType == BuffID.WellFed3)) { return true; }
            else return false;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var pos = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Consumable"); }
            tooltips.Insert(++index, SHIFTINFO);
            int exp = tooltips.FindIndex(line => line.Name == "WellFedExpert");
            if (exp != -1) { tooltips.RemoveAt(exp); }
            int d = tooltips.FindIndex(line => line.Name == "Tooltip2");
            if (d == -1) { d = tooltips.FindIndex(line => line.Name == "Tooltip1"); }
            if (d != -1)
            {
                var desc = tooltips[d];
                tooltips.RemoveAt(d);
                desc.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                var ind = 1 + tooltips.FindIndex(line => line.Name == "BuffTime");
                tooltips.Insert(ind, desc);
            }
            int s = tooltips.FindIndex(line => line.Name == "Tooltip2") != -1 ? tooltips.FindIndex(line => line.Name == "Tooltip1") : tooltips.FindIndex(line => line.Name == "Tooltip0");
            tooltips[s].Text = tooltips[s].Text.Replace(Language.GetTextValue("Mods.InformativeTooltips.Special.all"), Language.GetTextValue("Mods.InformativeTooltips.Special.various"));
            int i = tooltips.FindIndex(line => line.Name == "Tooltip2") != -1 ? tooltips.FindIndex(line => line.Name == "Tooltip0") : -1;
            if (i != -1)
            {
                var tip = tooltips[i];
                tooltips.RemoveAt(i);
                tip.OverrideColor = pos;
                tooltips.Insert(--i, tip);
            }

            if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
            {
                tooltips.Clear();
                string tip = "";
                if (item.buffType == BuffID.WellFed) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 2, 2, 5, 5, 0.5, 20, 5); }
                if (item.buffType == BuffID.WellFed2) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 3, 3, 7.5, 7.5, 0.75, 30, 10); }
                if (item.buffType == BuffID.WellFed3) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 4, 4, 10, 10, 1, 40, 15); }
                var Prov = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.InformativeTooltips.Special.provides"));
                var ExpNew = new TooltipLine(Mod, "Tooltip2", Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Expert"));
                var Hunger = new TooltipLine(Mod, "Tooltip3", Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Hunger"));
                ExpNew.OverrideColor = pos;
                Hunger.OverrideColor = pos;
                Prov.OverrideColor = pos;
                tooltips.Add(Prov);
                tooltips.Add(new(Mod, "Tooltip1", tip));
                if (Main.expertMode) { tooltips.Add(ExpNew); }
                if (Main.dontStarveWorld) { tooltips.Add(Hunger); }
            }
        }
    }
    public class WellFed : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && (type == BuffID.WellFed || type == BuffID.WellFed2 || type == BuffID.WellFed3))
            {
                var SHIFTINFO = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
                tip = $"{tip}\n{SHIFTINFO}";
                if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    if (type == BuffID.WellFed) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 1, 2, 5, 5, 0.5, 20, 5); }
                    if (type == BuffID.WellFed2) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 2, 3, 7.5, 7.5, 0.75, 30, 10); }
                    if (type == BuffID.WellFed3) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 3, 4, 10, 10, 1, 40, 15); }
                }
            }
        }
    }

    public class CampFireBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && type == BuffID.Campfire) { tip = Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip") + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenMult.Tooltip"), 1.1); }
        }
    }
    public class GlobalPlaceableBuffs : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public virtual bool DoApply { get; set; } = false;
        public virtual string Cond { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.grantsbuff");
        public virtual string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby");
        public virtual string Line1 { get; set; } = string.Empty;
        public virtual string Line2 { get; set; } = string.Empty;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return DoApply;
        }
        public virtual void ToModify(Item item, List<TooltipLine> tooltips, string cond, string line1, string line2)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true)
            {
                int index = tooltips.FindIndex(tip => tip.Name == "Tooltip0");
                if (index != -1)
                {
                    if (tooltips[index].Text.First() != '+' && tooltips[index].Text.First() != '-' && !Char.IsLetter(tooltips[index].Text.First()) && !Char.IsNumber(tooltips[index].Text.First())) { tooltips[index].OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor; ; tooltips.Insert(index, new(Mod, "Tooltip1", $"{cond} {Cond2}")); index = tooltips.FindIndex(tip => tip.Name == "Tooltip1"); }
                    else { tooltips[index].Text = $"{cond} {Cond2}"; }
                }
                else { tooltips.Add(new(Mod, "Tooltip0", $"{cond} {Cond2}")); index = tooltips.FindIndex(tip => tip.Name == "Tooltip0"); }
                var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
                SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    if (tooltips.FindIndex(tip => tip.Name == "Tooltip1") != -1) { tooltips[tooltips.FindIndex(tip => tip.Name == "Tooltip1")].Hide(); }
                    foreach (var line in tooltips)
                    {
                        if (line.Name != "ItemName" && line.Name != "Tooltip0") { line.Hide(); }
                    }
                    index = tooltips.FindIndex(tip => tip.Name == "ItemName");
                    tooltips[index].OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                    index = tooltips.FindIndex(tip => tip.Name == "Tooltip0");
                    string prov = Language.GetTextValue("Mods.InformativeTooltips.Special.provides");
                    tooltips[index].Text = prov.Insert(prov.IndexOf(':'), $" {Cond2}");
                    tooltips[index].OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                    if (line1 != string.Empty) { var Line1 = new TooltipLine(Mod, "Tooltip1", line1); tooltips.Insert(++index, Line1); }
                    if (line2 != string.Empty) { var Line2 = new TooltipLine(Mod, "Tooltip2", line2); tooltips.Insert(++index, Line2); }
                }
                else { tooltips.Insert(index, SHIFTINFO); }
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            ToModify(item, tooltips, Cond, Line1, Line2);
        }
    }
    public class GlobalCatBastTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.CatBast;
        }
        public override bool DoApply { get; set; } = true;
        public override string Line1 { get; set; } = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.DefenseInc.Tooltip"), 5);
    }
    public class GlobalCampfireTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.createTile == TileID.Campfire || item.createTile == TileID.Fireplace;
        }
        public override bool DoApply { get; set; } = true;
        public override string Line1 { get; set; } = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenMult.Tooltip"), 1.1);
        public override string Line2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip");
    }
    public class GlobalHeartLanternTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.HeartLantern;
        }
        public override bool DoApply { get; set; } = true;
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.Regen1.Tooltip");
    }
    public class GlobalGnomeTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.GardenGnome;
        }
        public override bool DoApply { get; set; } = true;
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.Gnome");
    }
    public class GlobalSunFlowerTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Sunflower;
        }
        public override bool DoApply { get; set; } = true;
        public override string Line1 { get; set; } = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.TooltipStat"), 10, Language.GetTextValue("Mods.InformativeTooltips.Class.movement"), Language.GetTextValue("Mods.InformativeTooltips.Stat.speed"));
        public override string Line2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.SunFlower");
    }
    public class GlobalStarInABottleTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.StarinaBottle;
        }
        public override bool DoApply { get; set; } = true;
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.StarBottle.1");
        public override string Line2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.StarBottle.2");
    }
    public class GlobalAmmoBoxTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.AmmoBox;
        }
        public override bool DoApply { get; set; } = true;
        public override string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby") + ' ' + Language.GetTextValue("Mods.InformativeTooltips.Special.clickbuff");
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.AmmoBox");
    }
    public class GlobalCrystalBallTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.CrystalBall;
        }
        public override bool DoApply { get; set; } = true;
        public override string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby") + ' ' + Language.GetTextValue("Mods.InformativeTooltips.Special.clickbuff");
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.CrystalBall");
    }
    public class GlobalBewitchingTableTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.BewitchingTable;
        }
        public override bool DoApply { get; set; } = true;
        public override string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby") + ' ' + Language.GetTextValue("Mods.InformativeTooltips.Special.clickbuff");
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.BewitchingTable");
    }
    public class GlobalWarTableTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.WarTable;
        }
        public override bool DoApply { get; set; } = true;
        public override string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby") + ' ' + Language.GetTextValue("Mods.InformativeTooltips.Special.clickbuff");
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.WarTable");
    }
    public class GlobalSharpeningStationTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SharpeningStation;
        }
        public override bool DoApply { get; set; } = true;
        public override string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby") + ' ' + Language.GetTextValue("Mods.InformativeTooltips.Special.clickbuff");
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.SharpeningStation");
    }
    public class GlobalSliceOfCakeTooltip : GlobalPlaceableBuffs
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SliceOfCake;
        }
        public override bool DoApply { get; set; } = true;
        public override string Cond2 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Special.nearby") + ' ' + Language.GetTextValue("Mods.InformativeTooltips.Special.clickbuff");
        public override string Line1 { get; set; } = Language.GetTextValue("Mods.InformativeTooltips.Buffs.SliceOfCake");
    }
    public class CatBastBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && type == BuffID.CatBast)
            {
                tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.DefenseInc.Tooltip"), 5);
            }
        }
    }
    public class BoC : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.BrainOfConfusion;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.RemoveAt(2);
            tooltips.RemoveAt(2);
            tooltips.RemoveAt(2);
            tooltips.Insert(2, new TooltipLine(Mod, "BOC1", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.basetooltip1")));
            tooltips.Insert(3, new TooltipLine(Mod, "BOC2", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.basetooltip2")));
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }

            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Prov = new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip2"));
                Prov.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                var dodgechance = new TooltipLine(Mod, "BOC3", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip1"));
                dodgechance.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                tooltips.Add(dodgechance);
                tooltips.Add(Prov);
                tooltips.Add(new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip3")));
                tooltips.Add(new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip4")));
                tooltips.Add(new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip5")));
            }
        }
    }
    public class AegisFruit : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.AegisFruit;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips[2].Text = $"{tooltips[2].Text} {Language.GetTextValue("Mods.InformativeTooltips.Items.AegisVanilla")}";
        }
    }
    public class AegisCrystal : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.AegisCrystal;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips[2].Text = $"{tooltips[2].Text.Remove(tooltips[2].Text.IndexOf(' '))} {string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenRampRate.Tooltip").ToLowerInvariant(), 20)}";
        }
    }
    public class AnkhS : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return (entity.type == ItemID.AnkhShield || entity.type == ItemID.AnkhCharm);
        }
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }
            tooltips.Insert(++index, new TooltipLine(Mod, "Immunities", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.basetooltip")));
            tooltips.RemoveAt(++index);
            if (item.type == ItemID.AnkhShield) { tooltips.RemoveAt(index); }
            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Provides = new TooltipLine(Mod, "ProvidesTo", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.providesto"));
                Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Add(Provides);
                if (item.type == ItemID.AnkhShield) { tooltips.Add(new(Mod, "AnkhList", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.shieldonly"))); }
                tooltips.Add(new(Mod, "AnkhList", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.list")));
            }
        }
    }
    public class MoonCharm : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.MoonCharm;
        }
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }
            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Provides = new TooltipLine(Mod, "NightTimeOnly", Language.GetTextValue("Mods.InformativeTooltips.Special.night"));
                Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Add(Provides);
                tooltips.Add(new(Mod, "MoonCharm", string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.Werewolf.Tooltip"), 3, 2, 5.1, 5.1, 5)));
                tooltips.Add(new(Mod, "RegenSlight", Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip")));
            }
        }
    }
    public class Rakushka : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NeptunesShell;
        }
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }
            tooltips.Insert(++index, new TooltipLine(Mod, "MerefolkWater", Language.GetTextValue("Mods.InformativeTooltips.Special.merefolk")));
            tooltips.RemoveAt(++index);
            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Provides = new TooltipLine(Mod, "Underwater", Language.GetTextValue("Mods.InformativeTooltips.Special.underwater"));
                Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Add(Provides);
                tooltips.Add(new(Mod, "rakushka", Language.GetTextValue("Mods.InformativeTooltips.Buffs.Merefolk.Tooltip")));
            }
        }
    }
    public class ScrollTooltipPlayer : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle)
            {
                if (!Main.keyState.IsKeyDown(Keys.LeftShift) && !Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    ScrollTooltip.Current = 1;
                    ScrollTooltip.scrolled = 0;
                }
                else
                {
                    if (PlayerInput.ScrollWheelDelta > 0) { ScrollTooltip.scrolled++; }
                    else if (PlayerInput.ScrollWheelDelta < 0) { ScrollTooltip.scrolled--; }

                    if (ScrollTooltip.scrolled <= -5) { ScrollTooltip.scrolled = 0; ScrollTooltip.Current = ScrollTooltip.Current != ScrollTooltip.max ? ++ScrollTooltip.Current : 1; }
                    else if (ScrollTooltip.scrolled >= 5) { ScrollTooltip.scrolled = 0; ScrollTooltip.Current = ScrollTooltip.Current != 1 ? --ScrollTooltip.Current : ScrollTooltip.max; }
                }
            }
        }
    }
    public static class ScrollTooltip
    {
        public static int max = 2;
        public static int Current = Math.Clamp(1, 1, max);
        public static int scrolled = Math.Clamp(0, 0, 5);
        public static bool PrevState = false;
    }
    public class CShell : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return (item.type == ItemID.CelestialShell || item.type == ItemID.MoonStone || item.type == ItemID.SunStone || item.type == ItemID.CelestialStone || item.type == ItemID.MoonShell);
        }
        public string incstats = Language.GetTextValue("Mods.InformativeTooltips.Special.incstats");
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public string night = Language.GetTextValue("Mods.InformativeTooltips.Special.night");
        public string day = Language.GetTextValue("Mods.InformativeTooltips.Special.day");
        public string provides = Language.GetTextValue("Mods.InformativeTooltips.Special.provides");
        public string regen = Language.GetTextValue("Mods.InformativeTooltips.Buffs.Regen1.Tooltip");
        public string slightregen = Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip");
        public string moveinfo = Language.GetTextValue("Mods.InformativeTooltips.Special.moveinfo");
        public string werewolf = Language.GetTextValue("Mods.InformativeTooltips.Special.werewolf");
        public string merefolk = Language.GetTextValue("Mods.InformativeTooltips.Special.merefolk");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!item.social)
            {
                if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
                {
                    tooltips.Clear();
                    if (item.type != ItemID.CelestialShell && item.type != ItemID.MoonShell)
                    {
                        if (item.type == ItemID.MoonStone)
                        {
                            var Provides = new TooltipLine(Mod, "NightTimeOnly", night);
                            Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                            tooltips.Add(Provides);
                        }
                        if (item.type == ItemID.SunStone)
                        {
                            var Provides = new TooltipLine(Mod, "DayTimeOnly", day);
                            Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                            tooltips.Add(Provides);
                        }
                        if (item.type == ItemID.CelestialStone)
                        {
                            var Provides = new TooltipLine(Mod, "ItemEffects", provides);
                            Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                            tooltips.Add(Provides);
                        }
                        tooltips.Add(new(Mod, "MoonStats", string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.MoonStone.Tooltip"), 4, 2, 10, 10, 0.5, 15)));
                        tooltips.Add(new(Mod, "IncreasedRegen", regen));
                    }
                    else
                    {
                        tooltips.Clear();
                        int y;
                        if (item.type == ItemID.MoonShell) { ScrollTooltip.max = 2; y = 1; }
                        else { ScrollTooltip.max = 3; y = 0; }

                        int CurrentLine = ScrollTooltip.Current + y;
                        var moveinf = new TooltipLine(Mod, "MoveWithArrows", moveinfo);
                        moveinf.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                        tooltips.Add(moveinf);
                        var Provides = new TooltipLine(Mod, "ProvidesCShell", provides);
                        Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                        tooltips.Add(Provides);
                        var WerewolfLine = new TooltipLine(Mod, "WerewolfLine", werewolf);
                        WerewolfLine.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                        var MerefolkLine = new TooltipLine(Mod, "MerefolkLine", merefolk);
                        MerefolkLine.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                        var MoonStoneLine = new TooltipLine(Mod, "MoonStoneLine", incstats);
                        MoonStoneLine.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;

                        if (CurrentLine == 2)
                        {
                            string Werewolf = "During nighttime:" + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.Werewolf.Tooltip"), 3, 2, 5.1, 5.1, 5);
                            var WerewolfTooltip = new TooltipLine(Mod, "WerewolfStats", Werewolf);
                            if (item.type == ItemID.CelestialShell) { tooltips.Add(MoonStoneLine); }
                            tooltips.Add(WerewolfTooltip);
                            tooltips.Add(new(Mod, "SlightRegen", slightregen));
                            tooltips.Add(MerefolkLine);
                        }
                        if (CurrentLine == 3)
                        {
                            string Merefolk = "While submerged in liquid:" + "\n" + Language.GetTextValue("Mods.InformativeTooltips.Buffs.Merefolk.Tooltip");
                            var MerefolkTooltip = new TooltipLine(Mod, "MerefolkStats", Merefolk);
                            if (item.type == ItemID.CelestialShell) { tooltips.Add(MoonStoneLine); }
                            tooltips.Add(WerewolfLine);
                            tooltips.Add(MerefolkTooltip);
                        }
                        if (CurrentLine == 1)
                        {
                            string MoonStone = "Always:" + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.MoonStone.Tooltip"), 4, 2, 10, 10, 0.5, 15);
                            var MoonStoneTooltip = new TooltipLine(Mod, "MoonStoneStats", MoonStone);
                            if (item.type == ItemID.CelestialShell)
                            {
                                tooltips.Add(MoonStoneTooltip);
                                tooltips.Add(new(Mod, "IncreasedRegen", regen));
                            }
                            tooltips.Add(WerewolfLine);
                            tooltips.Add(MerefolkLine);
                        }
                    }
                }
                else
                {
                    var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
                    SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                    int index = tooltips.FindIndex(line => line.Name == "Material");
                    if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
                    if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
                    if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
                    {
                        tooltips.Insert(++index, SHIFTINFO);
                    }

                    if (item.type == ItemID.CelestialShell || item.type == ItemID.MoonShell)
                    {
                        var MoonStoneLine = new TooltipLine(Mod, "MoonStoneLine", incstats);
                        var WerewolfLine = new TooltipLine(Mod, "WerewolfLine", werewolf);
                        var MerefolkLine = new TooltipLine(Mod, "MerefolkLine", merefolk);


                        if (item.type == ItemID.CelestialShell) { tooltips.Insert(++index, MoonStoneLine); }
                        tooltips.Insert(++index, WerewolfLine);
                        tooltips.Insert(++index, MerefolkLine);
                        if (item.type == ItemID.CelestialShell)
                        {
                            tooltips.RemoveAt(++index);
                            tooltips.RemoveAt(++index);
                            tooltips.RemoveAt(++index);
                            tooltips.RemoveAt(--index);
                            tooltips.RemoveAt(--index);
                        }
                        else
                        {
                            tooltips.RemoveAt(++index);
                        }
                    }
                    else
                    {
                        if (item.type == ItemID.MoonStone)
                        {
                            var MoonStoneLine = new TooltipLine(Mod, "MoonStoneLine", $"{night.Replace(':', ',')} {incstats.ToLower()}");
                            tooltips.Insert(++index, MoonStoneLine);
                        }
                        else if (item.type == ItemID.SunStone)
                        {
                            var SunStoneLine = new TooltipLine(Mod, "SunStoneLine", $"{day.Replace(':', ',')} {incstats.ToLower()}");
                            tooltips.Insert(++index, SunStoneLine);
                        }
                        else
                        {
                            var CStoneLine = new TooltipLine(Mod, "CStoneLine", incstats);
                            tooltips.Insert(++index, CStoneLine);
                        }
                        tooltips.RemoveAt(++index);
                        tooltips.RemoveAt(++index);
                        tooltips.RemoveAt(--index);
                    }
                }
            }
        }
    }
    public class SillyGoofyHomingBulletTooltip : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.ChlorophyteBullet;
        }
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int index = tooltips.FindIndex(line => line.Name == "ItemName");
            if (index != -2)
            {
                tooltips[index].Text = Language.GetTextValue("Mods.InformativeTooltips.Individual.ChloroBullet.text") + " " + Language.GetTextValue("Mods.InformativeTooltips.Individual.ChloroBullet.name");
            }
        }
        public int SkillIssueTimer = 69;
        public override void OnConsumedAsAmmo(Item ammo, Item weapon, Player player)
        {
            if (ammo.type == ItemID.ChlorophyteBullet) { --SkillIssueTimer; }
            if (SkillIssueTimer <= 0) 
            { 
                SkillIssueTimer = 69;
                var SkillIssue = new AdvancedPopupRequest();
                SkillIssue.DurationInFrames = 200;
                SkillIssue.Text = Language.GetTextValue("Mods.InformativeTooltips.Individual.ChloroBullet.text");
                SkillIssue.Color = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                PopupText.NewText(SkillIssue, player.position + new Vector2(-20, -20));
            }
        }
    }
}