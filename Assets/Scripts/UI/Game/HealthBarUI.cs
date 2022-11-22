using System.Text;
using TMPro;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI healthLabelValue;
    [SerializeField]
    private RectTransform healthFillBar;

    private float maxHealth;
    private StringBuilder healthTextBuilder;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        maxHealth = gameManager.GetMaxHealth();
        healthTextBuilder = new StringBuilder();

        gameManager.OnHealthChange += OnHealthChange;

        OnHealthChange(maxHealth);
    }

    private void OnHealthChange(float health)
    {
        maxHealth = gameManager.GetMaxHealth();

        healthTextBuilder.Append(health);
        healthTextBuilder.Append(@"\");
        healthTextBuilder.Append(maxHealth);

        healthLabelValue.text = healthTextBuilder.ToString();
        healthFillBar.offsetMax = new Vector2(-(300.0f - 300.0f * health / maxHealth), healthFillBar.offsetMax.y);

        healthTextBuilder.Clear();
    }
}
