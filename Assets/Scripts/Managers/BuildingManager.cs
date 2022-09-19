using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public static BuildingManager instance { get { return _instance; } }

    [SerializeField]
    private Tilemap backgroundTilemap;

    [SerializeField]
    private GameObject turretPrefab;

    [SerializeField]
    private TurretScriptableObject[] turretVariants;
    public List<TurretScriptableObject> availableTurrets;

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
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int tilePosition = backgroundTilemap.WorldToCell(worldPoint);

        TileBase tile = backgroundTilemap.GetTile(tilePosition);

        if (tile)
        {
            Vector2 worldPosition = new Vector2(tilePosition.x + 1.0f, tilePosition.y + 1.0f);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0.0f, LayerMask.GetMask("NonBuildable"));

            if (!hit)
            {
                BuildTurret(availableTurrets[0], worldPosition);
            }
        }
    }

    public void BuildTurret(TurretScriptableObject turretVariant, Vector3 position)
    {
        GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity);

        turret.GetComponent<Turret>().data = turretVariant;
    }
}
