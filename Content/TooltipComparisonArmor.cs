using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using InformativeTooltips.Content.GlobalTooltips;
using Terraria.Localization;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using InformativeTooltips.Common.Configs;

namespace InformativeTooltips.Content
{
    public class TooltipComparisonArmor : GlobalTooltipsBase
    {
        public TooltipComparisonArmor() : base(3) { }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return !item.social && !item.accessory && !item.vanity;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;

            var currentHelmetDefense = player.armor[0].defense;
            var currentChestplateDefense = player.armor[1].defense;
            var currentLeggingsDefense = player.armor[2].defense;
            if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
            {
                if (item.headSlot != -1 && !player.armor[0].IsAir)
                {
                    var defenseDifference = item.defense - currentHelmetDefense;
                    AddDefenseComparisonTooltip(tooltips, defenseDifference);
                }
                else if (item.bodySlot != -1 && !player.armor[1].IsAir)
                {
                    var defenseDifference = item.defense - currentChestplateDefense;
                    AddDefenseComparisonTooltip(tooltips, defenseDifference);
                }
                else if (item.legSlot != -1 && !player.armor[2].IsAir)
                {
                    var defenseDifference = item.defense - currentLeggingsDefense;
                    AddDefenseComparisonTooltip(tooltips, defenseDifference);
                }
            }
        }
        private void AddDefenseComparisonTooltip(List<TooltipLine> tooltips, int defenseDifference)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().ArmorCompare == true)
            {
                foreach (var line in tooltips)
                {
                    if (line.Name != "Defense") continue;
                    var comparisonText = "";
                    if (defenseDifference != 0)
                    {
                        comparisonText += defenseDifference > 0 ? $" (+{defenseDifference})" : $" ({defenseDifference})";
                    }

                    if (!string.IsNullOrEmpty(comparisonText))
                    {
                        line.Text += comparisonText;
                        line.OverrideColor = defenseDifference > 0 ? ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor : ModContent.GetInstance<ArmorDetailedConfig>().NegativeColor;
                    }
                    break;
                }
            }
        }
    }
    public class MaterialLineSwap : GlobalTooltipsBase
    {
        public MaterialLineSwap() : base(1) { }
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return !item.accessory && !item.vanity && (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1);
        }
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!Main.keyState.IsKeyDown(Keys.LeftShift) && !Main.keyState.IsKeyDown(Keys.RightShift) && ModContent.GetInstance<ArmorDetailedConfig>().ArmorCompare == true)
            {
                int MatIndex = tooltips.FindIndex(line => line.Name == "Material");
                if (MatIndex != -1)
                {
                    var Mat = tooltips[MatIndex];
                    tooltips.RemoveAt(MatIndex);
                    tooltips.Insert(MatIndex - 1, Mat);
                }
            }
        }
    }
    public class ThisIsTheOne : GlobalTooltipsBase
    {
        public ThisIsTheOne() : base(2) { }
        public string damage = Language.GetTextValue("Mods.InformativeTooltips.Stat.damage");
        public string crit = Language.GetTextValue("Mods.InformativeTooltips.Stat.crit");
        public string speed = Language.GetTextValue("Mods.InformativeTooltips.Stat.speed");
        public string regeneration = Language.GetTextValue("Mods.InformativeTooltips.Stat.regeneration");
        public string mana = Language.GetTextValue("Mods.InformativeTooltips.Stat.mana");
        public string manamax = Language.GetTextValue("Mods.InformativeTooltips.Stat.manamax");
        public string minions = Language.GetTextValue("Mods.InformativeTooltips.Stat.minions");
        public string sentries = Language.GetTextValue("Mods.InformativeTooltips.Stat.sentries");
        public string melee = Language.GetTextValue("Mods.InformativeTooltips.Class.melee");
        public string magic = Language.GetTextValue("Mods.InformativeTooltips.Class.magic");
        public string ranged = Language.GetTextValue("Mods.InformativeTooltips.Class.ranged");
        public string summon = Language.GetTextValue("Mods.InformativeTooltips.Class.summon");
        public string life = Language.GetTextValue("Mods.InformativeTooltips.Class.life");
        public string movement = Language.GetTextValue("Mods.InformativeTooltips.Class.movement");
        public string cost = Language.GetTextValue("Mods.InformativeTooltips.Class.cost");
        public string maxnum = Language.GetTextValue("Mods.InformativeTooltips.Class.maxnum");
        public string max = Language.GetTextValue("Mods.InformativeTooltips.Class.max");
        public string massive = Language.GetTextValue("Mods.InformativeTooltips.Special.massive");
        public string minor = Language.GetTextValue("Mods.InformativeTooltips.Special.minor");
        public string slight = Language.GetTextValue("Mods.InformativeTooltips.Special.slight");
        public string Aggro = Language.GetTextValue("Mods.InformativeTooltips.Special.Aggro");
        public string ammo = Language.GetTextValue("Mods.InformativeTooltips.Special.ammo");
        public string chanceto = Language.GetTextValue("Mods.InformativeTooltips.Special.chanceto");
        public string save = Language.GetTextValue("Mods.InformativeTooltips.Special.save");
        public string and = Language.GetTextValue("Mods.InformativeTooltips.Special.and");
        public string hps = Language.GetTextValue("Mods.InformativeTooltips.Special.hps");
        public string increased = Language.GetTextValue("Mods.InformativeTooltips.IncWord.increased");
        public string Increases = Language.GetTextValue("Mods.InformativeTooltips.IncWord.Increases");
        public string reduced = Language.GetTextValue("Mods.InformativeTooltips.IncWord.reduced");
        public string Grants = Language.GetTextValue("Mods.InformativeTooltips.Special.Grants");
        public string prevWordInc = "";
        public string prevWordClass = "";
        public string prevWordStat = "";
        public string prevWordNumber = "";
        public string CNumber;
        public string CClass;
        public string CStat;
        public string Number = "";
        public string Class = "";
        public string Stat = "";
        public int Count;
        public bool ISChar = false;
        public bool ISRegen = false;
        public string LineName = "";
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return !item.social && !item.accessory && !item.vanity && (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string storedAggro = "";
            for (int i = 0; i < tooltips.Count; i++)
            {
                var line = tooltips[i];
                if (line.Text.Contains(Aggro))
                {
                    storedAggro = line.Text;
                    tooltips.Remove(line);
                    break;
                }
            }
            // call for Line Separation
            LineSeparation(item, tooltips);
            LineSeparation(item, tooltips);
            // call for Tooltip Reading
            if (ModContent.GetInstance<ArmorDetailedConfig>().ArmorCompare == true) { TooltipReading(item, tooltips); }

            var AggroLine = new TooltipLine(Mod, "Aggro Line", storedAggro);
            if (storedAggro != "") { tooltips.Add(AggroLine); }
        }
        public void LineSeparation(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltipLine = tooltips[i];
                if (tooltipLine.Name != "SetBonus")
                {
                    if (tooltipLine.Text.Contains('&'))
                    {
                        int ind = tooltipLine.Text.IndexOf('&');
                        tooltipLine.Text.Replace('&', ' ');
                        tooltipLine.Text.Insert(ind, and);
                    }
                    if (tooltipLine.Text.Contains(and))
                    {
                        // getting rid of the (something)
                        if (tooltipLine.Text.Contains('('))
                        {
                            int index = tooltipLine.Text.IndexOf('(');
                            if (index >= 0)
                            {
                                tooltipLine.Text = tooltipLine.Text.Substring(0, index).TrimEnd();
                            }
                        }

                        // operating on the original line
                        string OriginalLine = tooltipLine.Text;
                        tooltips.RemoveAt(i);
                        string[] parts = OriginalLine.Split(new string[] { and }, StringSplitOptions.None);

                        string FirstLine = parts[0].Trim();
                        string SecondLine = parts[1].Trim();

                        // calling for line changes
                        Count = tooltips.Count;
                        LineName = "First";
                        SeparatedLinesOperating(item, tooltips, FirstLine, false, false);
                        LineName = "Second";
                        SeparatedLinesOperating(item, tooltips, SecondLine, false, false);
                        if (Count + 2 > tooltips.Count)
                        {
                            LineName = "First";
                            SeparatedLinesOperating(item, tooltips, FirstLine, false, false);
                        }

                        // resetting stats for the next item
                        prevWordInc = "";
                        prevWordStat = "";
                        prevWordClass = "";
                        prevWordNumber = "";
                    }
                }
            }
        }
        public void SeparatedLinesOperating(Item item, List<TooltipLine> tooltips, string Line, bool IsReader, bool IsCur)
        {
            string WordNumber = "";
            string WordInc = "";
            string WordClass = "";
            string WordStat = "";
            if (!IsReader)
            {
                WordInc = prevWordInc;
                WordClass = prevWordClass;
                WordStat = prevWordStat;
            }
            // WordInc and WordStat and WordClass and WordNumber
            // WordInc
            if (Line.Contains(increased)) { WordInc = increased; }
            else if (Line.StartsWith(Increases)) { WordInc = Increases; }
            
            else if (Line.Contains(reduced)) { WordInc = reduced; }
            else if (Line.Contains(ammo)) { WordInc = chanceto; WordClass = save; WordStat = ammo; }


            // WordClass
            if (Line.Contains(movement)) { WordClass = movement; }
            else if (Line.Contains(melee)) { WordClass = melee; }
            else if (Line.Contains(magic)) { WordClass = magic; }
            else if (Line.Contains(ranged)) { WordClass = ranged; }
            else if (Line.Contains(summon)) { WordClass = summon; }

            // WordStat
            if (Line.Contains(damage)) { WordStat = damage; }
            else if (Line.Contains(crit)) { WordStat = crit; }
            else if (Line.Contains(speed)) { WordStat = speed; }
            else if (Line.Contains(minions)) { WordClass = maxnum; WordStat = minions; }
            else if (Line.Contains(sentries)) { WordClass = maxnum; WordStat = sentries; }
            else if (Line.Contains($"{life} {regeneration}")) { WordClass = life; WordStat = regeneration; }
            else if (Line.Contains(mana))
            {
                if (Line.Contains(max)) { WordClass = max; WordStat = manamax; }
                else { WordStat = cost; WordClass = mana; }
            }

            // WordNumber
            if (WordNumber == "")
            {
                if (Line.Contains($"{life} {regeneration}"))
                {
                    WordNumber = Grants;
                    if (Line.Contains(minor) || Line.Contains(minor.Replace(minor.First().ToString(), string.Empty))) { WordInc = minor; }
                    else if (Line.Contains(massive) || Line.Contains(massive.Replace(massive.First().ToString(), string.Empty))) { WordInc = massive; }
                    else if (Line.Contains(slight) || Line.Contains(slight.Replace(slight.First().ToString(), string.Empty))) { WordInc = slight; }
                    else if (Line.Contains("0")) { WordInc = "0"; }
                    else { WordInc = increased; }
                }
                else
                {
                    Regex number = new Regex(@"\d+%?");
                    WordNumber = number.Match(Line).Value;
                }
                if (WordNumber == "") { WordNumber = prevWordNumber; }
            }
            if (!IsReader)
            {
                // filling in the necessary parts

                if (Line.Contains(manamax) || Line.Contains(minions) || Line.Contains(sentries)) { Line = $"{WordInc} {WordClass} {WordStat} {WordNumber}"; }
                else { Line = $"{WordNumber} {WordInc} {WordClass} {WordStat}"; }

                if (WordNumber != "" && WordInc != "" && WordClass != "" && WordStat != "")
                {
                    tooltips.Add(new TooltipLine(Mod, Name, Line));
                }

                prevWordNumber = WordNumber;
                prevWordInc = WordInc;
                prevWordClass = WordClass;
                prevWordStat = WordStat;
            }
            else
            {
                string AddNum = WordNumber;
                string AddClass = WordClass;
                string AddStat = WordStat;
                if (WordNumber.Contains(Grants))
                {
                    double perS = 2;
                    if (WordInc == massive) { perS = 4; }
                    else if (WordInc == minor) { perS = 1; }
                    else if (WordInc == slight) { perS = 0.5; }
                    else if (WordInc == "0") { perS = 0; }
                    AddNum = $"{perS} {hps}";
                }
                if (IsCur)
                {
                    CNumber = AddNum;
                    CClass = AddClass;
                    CStat = AddStat;
                }
                else
                {
                    Number = AddNum;
                    Class = AddClass;
                    Stat = AddStat;
                }
            }
        }
        public static List<TooltipLine> GetTooltipLines(Item item)
        {
            // Pulled from ItemFilters.FitsFilter

            // Have to reinitialize because ItemLoader.ModifyTooltips resizes the arrays
            string[] toolTipNames = new string[30];
            string[] toolTipLines = new string[30];
            bool[] unusedPrefixLine = new bool[30];
            bool[] unusedBadPrefixLines = new bool[30];
            int unusedYoyoLogo = 0;
            int unusedResearchLine = 0;

            int numLines = 1;
            float knockBack = item.knockBack;
            Main.MouseText_DrawItemTooltip_GetLinesInfo(item, ref unusedYoyoLogo, ref unusedResearchLine, knockBack, ref numLines, toolTipLines, unusedPrefixLine, unusedBadPrefixLines, toolTipNames, out _);
            var modifiedTooltipLines = ItemLoader.ModifyTooltips(item, ref numLines, toolTipNames, ref toolTipLines, ref unusedPrefixLine, ref unusedBadPrefixLines, ref unusedYoyoLogo, out _, -1);
            return modifiedTooltipLines;
        }
        public void TooltipReading(Item item, List<TooltipLine> tooltips)
        {
            var player = Main.LocalPlayer;
            Item equipped = null;
            bool IsEquippedNull = false;
            string storedAggro = "";

            // Determine the equipped item based on the slot type
            if (item.headSlot != -1)
            {
                equipped = player.armor[0];
            }
            else if (item.bodySlot != -1)
            {
                equipped = player.armor[1];
            }
            else if (item.legSlot != -1)
            {
                equipped = player.armor[2];
            }

            // Prevent an infinite loop by checking if the equipped item is valid and not the same as the current item
            if (equipped.type == item.type)
            {
                return;
            }
            if (equipped == null || equipped.IsAir)
            {
                IsEquippedNull = true;
            }
            List<TooltipLine> equippedTooltips = null;
            if (!IsEquippedNull) { equippedTooltips = GetTooltipLines(equipped); }
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material") + 1;
            if (index == 0) { index = tooltips.FindIndex(line => line.Name == "Equipable") + 1; }
            if (index == 0) { index = tooltips.FindIndex(line => line.Name == "Defense") - 1; }
            if (!item.social && index != -1) { tooltips.Insert(index, SHIFTINFO);

                if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    tooltips.Remove(SHIFTINFO);
                    var ArmorComp = new TooltipLine(Mod, "ArmorComparison", Language.GetTextValue("Mods.InformativeTooltips.Items.ArmorCompare"));
                    ArmorComp.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                    int yea = tooltips.FindIndex(line => line.Name == "ItemName");
                    tooltips[yea].OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                    tooltips.Insert(yea + 1, ArmorComp);
                    foreach (var tooltipLine in tooltips)
                    {
                        if (tooltipLine.Name != "SetBonus")
                        {
                            SeparatedLinesOperating(item, tooltips, tooltipLine.Text, true, false);

                            double Diff = 0;
                            bool ISAdded = false;
                            if (!IsEquippedNull)
                            {
                                foreach (var equippedTooltip in equippedTooltips)
                                {
                                    SeparatedLinesOperating(equipped, equippedTooltips, equippedTooltip.Text, true, true);
                                    if (CStat == Stat && CClass == Class)
                                    {
                                        ISChar = false;
                                        ISRegen = false;
                                        int CharInDex = CNumber.IndexOf('%');
                                        int CharIndex = Number.IndexOf('%');
                                        int hpInDex = CNumber.IndexOf($" {hps.First()}");
                                        int hpIndex = Number.IndexOf($" {hps.First()}");

                                        if (CharInDex != -1 && CharIndex != -1)
                                        {
                                            ISChar = true;
                                            if (double.TryParse(CNumber.Remove(CharInDex), out double CNum) && double.TryParse(Number.Remove(CharIndex), out double Num))
                                            {
                                                Diff = Num - CNum;
                                            }
                                        }
                                        else if (hpInDex != -1 && hpIndex != -1)
                                        {
                                            ISRegen = true;
                                            if (double.TryParse(CNumber.Remove(hpInDex), out double CNum) && double.TryParse(Number.Remove(hpIndex), out double Num))
                                            {
                                                Diff = Num - CNum;
                                            }
                                        }
                                        else if (double.TryParse(CNumber, out double CNum) && double.TryParse(Number, out double Num))
                                        {
                                            Diff = Num - CNum;
                                        }
                                        AddStatTooltipComparison(tooltips, Diff, $"{Class} {Stat}");
                                        ISAdded = true;
                                        break;
                                    }
                                }
                                if (!ISAdded)
                                {
                                    if (tooltipLine.Text.Contains(Aggro))
                                    {
                                        storedAggro = tooltipLine.Text;
                                        tooltips.Remove(tooltipLine);
                                    }
                                    else
                                    {
                                        ISChar = false;
                                        ISRegen = false;
                                        int CharIndex = Number.IndexOf('%');
                                        int hpIndex = Number.IndexOf($" {hps}");
                                        if (CharIndex != -1)
                                        {
                                            ISChar = true;
                                            if (int.TryParse(Number.Remove(CharIndex), out int Num))
                                            {
                                                Diff = Num;
                                            }
                                        }
                                        else if (hpIndex != -1)
                                        {
                                            ISRegen = true;
                                            if (int.TryParse(Number.Remove(hpIndex), out int Num))
                                            {
                                                Diff = Num;
                                            }
                                        }
                                        else if (int.TryParse(Number, out int Num))
                                        {
                                            Diff = Num;
                                        }
                                        AddStatTooltipComparison(tooltips, Diff, $"{Class} {Stat}");
                                    }
                                }
                            }
                        }
                        if (tooltipLine.Name == "Equipable" || tooltipLine.Name == "Material" || tooltipLine.Name == "FavoriteDesc" || tooltipLine.Name == "Favorite")
                        {
                            tooltipLine.Hide();
                        }
                    }
                    if (!IsEquippedNull)
                    {
                        if (ModContent.GetInstance<ArmorDetailedConfig>().ArmorDetailsToggle == true) { AddingNonexistent(tooltips, item, equipped); }
                    }
                }
            }
        }
        private void AddStatTooltipComparison(List<TooltipLine> tooltips, double difference, string statName)
        {
            foreach (var line in tooltips)
            {
                if (!line.Text.Contains(statName) || !line.Text.Contains(Class) || line.Text.Contains('(')) {  continue; }
                var comparisonText = "";
                if (difference != 0)
                {
                    if (!ISChar && !ISRegen) { comparisonText = difference > 0 ? $" (+{difference})" : $" ({difference})"; }
                    else if (ISRegen) { comparisonText = difference > 0 ? $" (+{difference} {hps})" : $" ({difference} {hps})"; }
                    else { comparisonText = difference > 0 ? $" (+{difference}%)" : $" ({difference}%)"; }
                }

                if (!string.IsNullOrEmpty(comparisonText))
                {
                    line.Text += comparisonText;
                    line.OverrideColor = difference > 0 ? ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor : ModContent.GetInstance<ArmorDetailedConfig>().NegativeColor;
                }
                break;
            }
        }
        public void AddingNonexistent(List<TooltipLine> tooltips, Item item, Item equipped)
        {
            var equippedTooltips = GetTooltipLines(equipped);
            foreach (var line in equippedTooltips)
            {
                SeparatedLinesOperating(equipped, equippedTooltips, line.Text, true, true);
                bool DoAdd = true;
                foreach (var tooltip in tooltips)
                {
                    SeparatedLinesOperating(item, tooltips, tooltip.Text, true, false);
                    if (CStat == Stat && CClass == Class)
                    {
                        DoAdd = false; break;
                    }
                }
                if (DoAdd == true)
                {
                    var ToAdd = line.Text;
                    if (ToAdd.Contains(life))
                    {
                        if (ToAdd.Contains(increased)) { ToAdd = ToAdd.Replace(increased, "0"); }
                        if (ToAdd.Contains(minor)) { ToAdd = ToAdd.Replace(minor, "0"); }
                        if (ToAdd.Contains(massive)) { ToAdd = ToAdd.Replace(massive, "0"); }
                        if (ToAdd.Contains(slight)) { ToAdd = ToAdd.Replace(slight, "0"); }
                    }
                    ToAdd = Regex.Replace(ToAdd, @"\d", "0");
                    ToAdd = Regex.Replace(ToAdd, @"0+", "0");
                    tooltips.Add(new TooltipLine(Mod, "AddedComparison", ToAdd));
                    int Diff = 0;
                    ISChar = false;
                    ISRegen = false;
                    int CharIndex = CNumber.IndexOf('%');
                    int hpIndex = CNumber.IndexOf($" {hps}");
                    if (CharIndex != -1)
                    {
                        ISChar = true;
                        if (int.TryParse(CNumber.Remove(CharIndex), out int Num))
                        {
                            Diff = -Num;
                        }
                    }
                    else if (hpIndex != -1)
                    {
                        ISRegen = true;
                        if (int.TryParse(CNumber.Remove(hpIndex), out int Num))
                        {
                            Diff = -Num;
                        }
                    }
                    else if (int.TryParse(CNumber, out int Num))
                    {
                        Diff = -Num;
                    }
                    SeparatedLinesOperating(item, tooltips, ToAdd, true, false);
                    AddStatTooltipComparison(tooltips, Diff, $"{CClass} {CStat}");
                }
            }
        }
    }
}