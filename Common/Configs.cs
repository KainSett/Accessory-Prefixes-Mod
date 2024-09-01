using Terraria.ModLoader.Config;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace InformativeTooltips.Common.Configs
{
    public class ArmorDetailedConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("Tooltips")]
        [DefaultValue(false)]
        public bool ArmorCompare;
        public bool ArmorDetailsToggle;
        public bool SetBonusInInv;
        public bool AccessoryStatsToggle;
        public bool BuffDetailsToggle;
        public bool SmallDetailsToggle;
        [Header("Colors")]
        [DefaultValue(typeof(Color), "135, 206, 250, 255")]
        public Color HeaderColor;
        [DefaultValue(typeof(Color), "144, 238, 144, 255")]
        public Color PositiveColor;
        [DefaultValue(typeof(Color), "238, 144, 144, 255")]
        public Color NegativeColor;
        [DefaultValue(typeof(Color), "128, 128, 128, 255")]
        public Color NeutralColor;
    }
}