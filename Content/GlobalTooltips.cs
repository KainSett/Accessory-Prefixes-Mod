using Terraria.ModLoader;

namespace InformativeTooltips.Content.GlobalTooltips
{
    public class GlobalTooltipsBase : GlobalItem
    {
        public int Order { get; set; }

        protected GlobalTooltipsBase(int order)
        {
            Order = order;
        }
    }
    public class ManualTooltipFixes : GlobalTooltipsBase
    {
        public ManualTooltipFixes() : base(1) { }
    }
}