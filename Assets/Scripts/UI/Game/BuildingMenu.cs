using TMPro;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    private static BuildingMenu _instance;
    public static BuildingMenu instance { get { return _instance; } }

    private GameManager gameManager;

    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI neonBlocksLabel;

    [SerializeField]
    private GameObject buildingTurretUIPrefab;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

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

    public void Show()
    {
        animator.SetBool("isOpen", true);
    }

    public void Hide()
    {
        animator.SetBool("isOpen", false);
    }
}
