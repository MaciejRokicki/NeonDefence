using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public static BuildingManager instance { get { return _instance; } }

    private UIManager uiManager;

    [SerializeField]
    private Tilemap backgroundTilemap;

    [SerializeField]
    private GameObject turretPrefab;

    [SerializeField]
    private TurretScriptableObject[] turretVariants;
    public List<TurretScriptableObject> availableTurrets;

    private bool isTurretInfoVisible = false;

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

        uiManager = UIManager.instance;
    }

    private void Start()
    {
        availableTurrets = new List<TurretScriptableObject>();

        for(int i = 0; i < turretVariants.Length; i++)
        {
            availableTurrets.Add(turretVariants[i]);
        }
    }

    public void GetTile(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition = backgroundTilemap.WorldToCell(worldPoint);

            TileBase tile = backgroundTilemap.GetTile(tilePosition);

            if (tile)
            {
                Vector2 worldPosition = new Vector2(tilePosition.x + 1.0f, tilePosition.y + 1.0f);
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0.0f, LayerMask.GetMask("NonBuildable"));

                if (!hit)
                {
                    if(!isTurretInfoVisible)
                    {
                        BuildTurret(availableTurrets[0], worldPosition);
                    }
                    else
                    {
                        isTurretInfoVisible = false;
                        uiManager.HideTurretInfo();
                    }
                }
                else if (hit.collider.tag == "Turret")
                {
                    isTurretInfoVisible = true;
                    uiManager.ShowTurretInfo(hit.collider.GetComponent<Turret>());
                }
            }
            else if(isTurretInfoVisible)
            {
                isTurretInfoVisible = false;
                uiManager.HideTurretInfo();
            }
        }
    }

    private void BuildTurret(TurretScriptableObject turretVariant, Vector3 position)
    {
        GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity);

        turret.GetComponent<Turret>().variant = turretVariant;
    }
}
