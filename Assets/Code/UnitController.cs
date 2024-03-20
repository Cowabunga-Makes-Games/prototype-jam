using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] private int maxNumEnemies = 2;
    [SerializeField] private int numEnemies = 0;

    public GameObject chargeEnemyPrefab;

    GameObject selectedUnit;
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
        
        while (numEnemies < maxNumEnemies)
        {
            spawnEnemy();
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if(hasHit)
            {  
                Debug.Log(hit.transform.tag.ToString());
                if(hit.transform.tag == "Tile")
                {   
                    Debug.Log("Tile hit!");
                    // if(unitSelected)
                    // {
                    //     Vector2Int targetCords = hit.transform.GetComponent<GridLabeller>().pos;
                    //     Vector2Int startCords = new Vector2Int((int) selectedUnit.position.x, (int) selectedUnit.position.y) / gridManager.UnityGridSize;
                    //
                    //     selectedUnit.transform.position = new Vector3(targetCords.x, selectedUnit.position.y, targetCords.y);
                    // }
                }

                if(hit.transform.tag == "Enemy")
                {
                    selectedUnit = hit.transform.gameObject;
                    unitSelected = true;
                    Debug.Log("Enemy hit!");
                    
                    // initiate death sequence
                    Enemy enemy = selectedUnit.GetComponent<Enemy>();
                    if (enemy.initateDeath())
                    {
                        numEnemies--;
                    }
                    
                }
            }
        }
    }

    public void spawnEnemy()
    {   
        PosNode tile = gridManager.findUnoccupiedTile();
        GameObject enemy = Instantiate(chargeEnemyPrefab, new Vector3(tile.Pos.x, 0.5f, tile.Pos.y), Quaternion.identity);
        enemy.GetComponent<ChargeEnemy>().Initialize(tile);
        tile.setOccupant(enemy.GetComponent<ChargeEnemy>());

        numEnemies++;
    }
}