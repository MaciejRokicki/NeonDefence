using TMPro;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI neonBlocksLabel;

    [SerializeField]
    private GameObject buildingTurretUIPrefab;

    private void Awake()
    {
        gameManager = GameManager.instance;
        animator = GetComponent<Animator>();

        gameManager.OnNeonBlockChange += OnNeonBlocksChange;
    }

    private void Start()
    {
        OnNeonBlocksChange(gameManager.GetNeonBlocks());
    }
    
    private void OnNeonBlocksChange(int neonBlocks)
    {
        neonBlocksLabel.text = neonBlocks.ToString();
    }

    public void ToggleMenu()
    {
        animator.SetBool("isOpen", !animator.GetBool("isOpen"));
    }
}
