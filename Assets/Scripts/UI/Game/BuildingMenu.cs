using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    BuildingManager buildingManager;

    [SerializeField]
    private GameObject buildingTurretUIPrefab;
    [SerializeField]
    private GameObject turretPlaceholder;
    private TurretScriptableObject selectedVariant;

    private void Start()
    {
        buildingManager = BuildingManager.instance;

        buildingManager.OnAvailableTurrestLoad += PrepareTurretsMenu;
    }

    private void PrepareTurretsMenu()
    {
        for (int i = 0; i < buildingManager.availableTurrets.Count; i++)
        {
            TurretScriptableObject variant = buildingManager.availableTurrets[i];
            GameObject buildingTurret = Instantiate(buildingTurretUIPrefab, transform);

            buildingTurret.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150.0f + (100.0f * i - 1), 0.0f);
            buildingTurret.GetComponent<BuildingTurretUI>().variant = variant;
            buildingTurret.GetComponent<Image>().sprite = variant.turretIcon;
            buildingTurret.GetComponent<Image>().material = variant.turretIconMaterial;
        }
    }    

    private void Update()
    {
        if (selectedVariant)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            buildingManager.GetTurretBuildingTile(worldPoint, out tilePosition);

            turretPlaceholder.transform.position = tilePosition;
        }
    }

    public void SelectVariant(TurretScriptableObject variant)
    {
        PrepareTurretPlaceholder(variant);
        selectedVariant = variant;
    }

    public void BuildTurret(InputAction.CallbackContext ctxt)
    {
        if (selectedVariant && ctxt.canceled)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            UnityEngine.Tilemaps.TileBase tile = buildingManager.GetTurretBuildingTile(worldPoint, out tilePosition);

            if (tile)
            {
                buildingManager.BuildTurret(selectedVariant, tilePosition);
            }

            turretPlaceholder.SetActive(false);
            selectedVariant = null;
        }
    }

    private void PrepareTurretPlaceholder(TurretScriptableObject variant)
    {
        turretPlaceholder.SetActive(true);
        turretPlaceholder.GetComponent<TurretPlaceholder>().SetTurretVariant(variant);
        turretPlaceholder.GetComponent<SpriteRenderer>().sprite = variant.turretSprite;
        turretPlaceholder.GetComponent<SpriteMask>().sprite = variant.turretSprite;
    }
}
