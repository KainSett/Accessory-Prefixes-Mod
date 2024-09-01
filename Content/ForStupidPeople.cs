using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InformativeTooltips.Content.ForStupidPeople
{
    public class StonesDowngrade : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.CelestialStone || entity.type == ItemID.MoonStone || entity.type == ItemID.SunStone || entity.type == ItemID.CelestialShell || entity.type == ItemID.MoonCharm;
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if ((equippedItem.type == ItemID.CelestialStone || equippedItem.type == ItemID.MoonStone || equippedItem.type == ItemID.SunStone || equippedItem.type == ItemID.CelestialShell) && (incomingItem.type == ItemID.CelestialStone || incomingItem.type == ItemID.MoonStone || incomingItem.type == ItemID.SunStone || incomingItem.type == ItemID.CelestialShell)) { return false; }
            if ((equippedItem.type == ItemID.CelestialShell || equippedItem.type == ItemID.MoonCharm) && (incomingItem.type == ItemID.CelestialShell || incomingItem.type == ItemID.MoonCharm)) { return false; }
            return true;
        }
    }
}