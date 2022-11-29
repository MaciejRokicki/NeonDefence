namespace Assets.Scripts.InRunUpgradre
{
    public abstract class InRunUpgradeCreationToolStrategy
    {
        public abstract void OnGui();
        public abstract void Create(string upgradeName, bool unique, TierScriptableObject tier);
    }
}