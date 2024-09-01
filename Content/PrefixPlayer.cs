using AccessoryPrefixesPlus.Content.StatPrefixes;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AccessoryPrefixesPlus.Content.UniquePrefixes;

namespace AccessoryPrefixesPlus.Content.PrefixPlayer
{
    public class AccessoryPrefixPlayer : ModPlayer
    {
        public float size = 0f;
        public float spawnrate = 1f;
        public override void PostUpdateEquips()
        {
            size = 0f;
            spawnrate = 1;
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
            }
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