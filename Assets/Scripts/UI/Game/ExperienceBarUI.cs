using UnityEngine;

public class ExperienceBarUI : MonoBehaviour
{
    private UpgradeManager upgradeManager;

    [SerializeField]
    private RectTransform experienceFillBar;

    private void Awake()
    {
        upgradeManager = UpgradeManager.instance;
    }

    private void Start()
    {
        upgradeManager.OnExperienceChange += OnHealthChange;
    }

    private void OnHealthChange(int experience, int experienceToNextRoll)
    {
        experienceFillBar.offsetMax = new Vector2(-(500.0f - 500.0f * experience / experienceToNextRoll), experienceFillBar.offsetMax.y);
    }
}
