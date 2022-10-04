using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    GameManager gameManager;
    BuildingManager buildingManager;

    [SerializeField]
    private TextMeshProUGUI neonBlocksLabel;

    [SerializeField]
    private GameObject buildingTurretUIPrefab;

    private List<BuildingTurretUI> buildingTurretsUI;

    [SerializeField]
    private Vector2 buildingTurretSize = new Vector2(60.0f, 60.0f);
    [SerializeField]
    private float containerMargin = 20;
    [SerializeField]
    private float buildingTurretMargin = 10;
    [SerializeField]
    private int buildingTurretsInRow = 3;

    private void Awake()
    {
        gameManager = GameManager.instance;
        buildingManager = BuildingManager.instance;

        gameManager.OnNeonBlockChange += OnNeonBlocksChange;
    }

    private void Start()
    {
        buildingTurretsUI = new List<BuildingTurretUI>();

        PrepareTurretsMenu();

        OnNeonBlocksChange(gameManager.GetNeonBlocks());
    }

    private void PrepareTurretsMenu()
    {
        Vector2 containerSize = Vector2.zero;

        int turretsInRow = buildingTurretsInRow < buildingManager.availableTurrets.Count ? buildingTurretsInRow : buildingManager.availableTurrets.Count;
        int rows = Mathf.CeilToInt((float)buildingManager.availableTurrets.Count / buildingTurretsInRow);

        containerSize.x += 2 * containerMargin + turretsInRow * buildingTurretSize.x + (turretsInRow - 1) * buildingTurretMargin;
        containerSize.y += 2 * containerMargin + rows * buildingTurretSize.y + (rows - 1) * buildingTurretMargin;

        GetComponent<RectTransform>().sizeDelta = containerSize;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-containerSize.x / 2 - 10.0f, containerSize.y / 2 + 10.0f);

        for (int i = 0, j = 0, k = 0; i < buildingManager.availableTurrets.Count; i++, k++)
        {
            if(k > buildingTurretsInRow - 1)
            {
                j++;
                k = 0;
            }

            TurretScriptableObject variant = buildingManager.availableTurrets[i];
            GameObject buildingTurret = Instantiate(buildingTurretUIPrefab, transform);

            buildingTurret.GetComponent<RectTransform>().sizeDelta = buildingTurretSize;
            buildingTurret.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    -containerSize.x / 2 + buildingTurretSize.x / 2 + containerMargin + (i % buildingTurretsInRow) * (buildingTurretSize.x + buildingTurretMargin),
                    containerSize.y / 2 - containerMargin - buildingTurretSize.y / 2 - j * (buildingTurretSize.y + buildingTurretMargin)
                );

            buildingTurret.GetComponent<BuildingTurretUI>().variant = variant;

            buildingTurretsUI.Add(buildingTurret.GetComponent<BuildingTurretUI>());
        }
    }
    
    private void OnNeonBlocksChange(int neonBlocks)
    {
        foreach(BuildingTurretUI buildingTurretUI in buildingTurretsUI)
        {
            if(neonBlocks >= buildingTurretUI.variant.cost)
            {
                buildingTurretUI.SetAvailableToPurchase();
            }
            else
            {
                buildingTurretUI.SetUnavailableToPurchase();
            }
        }

        neonBlocksLabel.text = neonBlocks.ToString();
    }
}
