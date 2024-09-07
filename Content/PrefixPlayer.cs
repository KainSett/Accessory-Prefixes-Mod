using AccessoryPrefixesPlus.Content.StatPrefixes;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AccessoryPrefixesPlus.Content.UniquePrefixes;
using AccessoryPrefixesPlus.Common.Configs;
using System.Linq;

namespace AccessoryPrefixesPlus.Content.PrefixPlayer
{
    public class AccessoryPrefixPlayer : ModPlayer
    {
        public float size = 0f;
        public float spawnrate = 1f;
        public float power = 0f;
        public int breathTime = 0;
        public int breathCD = 0;
        public int prevBreathTime = 0;
        public int prevBreathCD = 0;
        public override void UpdateEquips()
        {
            Player.breathMax += breathTime - prevBreathTime;
            prevBreathTime = breathTime;
            var color = ModContent.GetInstance<TheConfig>().RoyalLightColor;
            if (power != 0f) { Lighting.AddLight(Player.Center, color.R / 100 * power, color.G / 100 * power, color.B / 100 * power); }
        }
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (item.DamageType == DamageClass.Melee)
            {
                scale += size;
            }
        }
        public override void ResetEffects()
        {
            size = 0f;
            spawnrate = 1f;
            power = 0f;
            breathTime = 0;
            breathCD = 0;
        }
    }

    public class NPCSpawnRatePrefix : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            spawnRate = (int)ModContent.GetInstance<AccessoryPrefixPlayer>().spawnrate * spawnRate;
        }
    }
}