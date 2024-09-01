using Terraria.ModLoader.Config;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace AccessoryPrefixesPlus.Common.Configs
{
    public class TheConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("Config")]
        [DefaultValue(typeof(Color), "255, 215, 0, 255")]
        public Color RoyalLightColor;
    }
}