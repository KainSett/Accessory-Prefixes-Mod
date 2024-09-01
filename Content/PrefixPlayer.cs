using AccessoryPrefixesPlus.Content.StatPrefixes;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AccessoryPrefixesPlus.Content.UniquePrefixes;
using AccessoryPrefixesPlus.Common.Configs;

namespace AccessoryPrefixesPlus.Content.PrefixPlayer
{
    public class AccessoryPrefixPlayer : ModPlayer
    {
        public float size = 0f;
        public float spawnrate = 1f;
        public float power = 0f;
        public override void PostUpdateEquips()
        {
            size = 0f;
            spawnrate = 1f;
            power = 0f;
            foreach (var item in Player.armor)
            {
                if (item.social)
                {
                    continue;
                }
                if (item.prefix == ModContent.PrefixType<Masterful>())
                {
                    size += 0.04f;
                }
                if (item.prefix == ModContent.PrefixType<Peaceful>())
                {
                    spawnrate += 0.04f;
                }
                if (item.prefix == ModContent.PrefixType<Royal>())
                {
                    power = power == 0 ? 0.1f : power * 1.5f;
                }
            }
            var color = ModContent.GetInstance<TheConfig>().RoyalLightColor;
            Lighting.AddLight(Player.Center, color.R / 100 * power, color.G / 100 * power, color.B / 100 * power);
        }
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (item.DamageType == DamageClass.Melee)
            {
                scale += size;
            }
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