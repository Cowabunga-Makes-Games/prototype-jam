using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    Transform selectedUnit;
    bool unitSelected = false;

    GridManager gridManager;


    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if(hasHit)
            {   Debug.Log("The tag is: " + hit.transform.tag);
                if(hit.transform.tag == "Tile")
                {   
                    Debug.Log("Tile hit!");
                    if(unitSelected)
                    {
                        Vector2Int targetCords = hit.transform.GetComponent<GridLabeller>().pos;
                        Vector2Int startCords = new Vector2Int((int) selectedUnit.position.x, (int) selectedUnit.position.y) / gridManager.UnityGridSize;

                        selectedUnit.transform.position = new Vector3(targetCords.x, selectedUnit.position.y, targetCords.y);
                    }
                }

                if(hit.transform.tag == "Unit")
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                    Debug.Log("Unit hit!");
                }
            }
        }
    }
}