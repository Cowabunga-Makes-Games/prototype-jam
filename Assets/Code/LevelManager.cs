using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int numMaxEnemies;

    private GridManager _gridManager;
    private int _numEnemies = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if numEnemies < numMaxEnmemies then generate numMaxEnemies - numEnemies enemies into random positions
        // on the platform
        if (_numEnemies < numMaxEnemies)
        {
            // generate numMaxEnemies - numEnemies number of enemes into random positions on the grid platform.
            throw new NotImplementedException();
        }
        
        // if an enemy is clicked, destroy it and decrement _numEnemies accordingly
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    // initiate death sequence in enemy
                    throw new NotImplementedException();
                }
            }
            
            throw new NotImplementedException();
        }
    }
}
