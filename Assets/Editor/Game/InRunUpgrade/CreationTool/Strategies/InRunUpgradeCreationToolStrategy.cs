namespace Assets.Editor.Game.InRunUpgrade.CreationTool.Strategies
{
    public abstract class InRunUpgradeCreationToolStrategy
    {
        protected UpgradeManager upgradeManager;

        public InRunUpgradeCreationToolStrategy(UpgradeManager upgradeManager)
        {
            this.upgradeManager = upgradeManager;
        }

        public abstract void OnGui();
        public abstract void Create(string upgradeName, bool unique, TierScriptableObject tier, string description);
    }
}