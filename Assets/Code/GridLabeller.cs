using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridLabeller : MonoBehaviour
{
    private TextMeshPro label;
    public Vector2Int pos = new Vector2Int();
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        DisplayPosition();
    }

    private void Update()
    {
        DisplayPosition();
        transform.name = pos.ToString();
    }

    private void DisplayPosition()
    {
        if (!gridManager)
        {
            return;
        }

        pos.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        pos.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);

        label.text = $"{pos.x}, {pos.y}";
    }
}
